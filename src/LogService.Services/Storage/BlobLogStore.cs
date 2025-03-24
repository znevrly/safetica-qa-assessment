using System.Text.Json;
using Azure.Storage.Blobs;
using LogService.Services.Models;

namespace LogService.Services.Storage
{
    /// <summary>
    /// A persistent log store implementation using Azure Blob Storage.
    /// </summary>
    public class BlobLogStore : ILogStore
    {
        private const string LogsContainerName = "safetica-logs";

        private readonly BlobServiceClient _client;

        public BlobLogStore(BlobServiceClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task AddLogAsync(Log log, CancellationToken cancellationToken)
        {
            var container = await GetContainerAsync(cancellationToken);
            var blob = container.GetBlobClient(log.Id.ToString());

            // Check if the blob already exists
            if (await blob.ExistsAsync(cancellationToken))
            {
                throw new InvalidOperationException($"A log with ID {log.Id} already exists.");
            }

            var json = JsonSerializer.Serialize(log);
            await blob.UploadAsync(BinaryData.FromString(json), overwrite: false, cancellationToken);
        }

        public async Task<IEnumerable<Log>> GetLogsAsync(CancellationToken cancellationToken)
        {
            var container = await GetContainerAsync(cancellationToken);
            var pages = container.GetBlobsAsync(cancellationToken: cancellationToken).AsPages();
            var result = new List<Log>();
            await foreach (var page in pages.WithCancellation(cancellationToken))
            {
                foreach (var item in page.Values)
                {
                    var blob = container.GetBlobClient(item.Name);
                    using var stream = await blob.OpenReadAsync(cancellationToken: cancellationToken);
                    var log = await JsonSerializer.DeserializeAsync<Log>(stream, cancellationToken: cancellationToken) 
                        ?? throw new JsonException("Invalid model");

                    result.Add(log);
                }
            }

            return result;
        }

        public async Task<bool> DeleteLogIfExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            var container = await GetContainerAsync(cancellationToken);

            var blob = container.GetBlobClient(id.ToString());
            var result = await blob.DeleteIfExistsAsync(cancellationToken: cancellationToken);
            return result.Value;
        }

        private async Task<BlobContainerClient> GetContainerAsync(CancellationToken cancellationToken)
        {
            var container = _client.GetBlobContainerClient(LogsContainerName);
            await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
            return container;
        }
    }
}
