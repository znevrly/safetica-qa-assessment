using System.Net.Http.Json;
using System.Text.Json;
using LogService.Api.Models;

namespace LogService.Api
{
    public class LogClient : ILogClient
    {
        private static readonly Uri LogsEndpoint = new Uri("logs", UriKind.Relative);

        private readonly HttpClient _httpClient;

        public LogClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<LogModel>> GetLogsAsync(CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, LogsEndpoint);
            using var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<LogModel>>(cancellationToken: cancellationToken) 
                ?? throw new JsonException("Invalid model");
        }

        public async Task<LogModel> AddLogAsync(LogAddModel log, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, LogsEndpoint);
            request.Content = new StringContent(JsonSerializer.Serialize(log));

            using var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<LogModel>(cancellationToken: cancellationToken) 
                ?? throw new JsonException("Invalid model");
        }

        public async Task DeleteLogAsync(Guid id, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, LogsEndpoint.OriginalString + $"/{id}");

            using var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
    }
}