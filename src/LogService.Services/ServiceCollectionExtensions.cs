using LogService.Services.Storage;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LogService.Services
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all required services to the service collection, unless already registered.
        /// </summary>
        /// <param name="services">the service collection</param>
        /// <param name="blobStorageConnectionString">an Azure Blob Storage connection string for the log store</param>
        public static IServiceCollection AddLogServices(this IServiceCollection services, string blobStorageConnectionString)
        {
            services.TryAddScoped<ILogService, LogService>();

            if (!services.Any(x => x.ServiceType == typeof(ILogStore)))
            {
                services.AddScoped<ILogStore, BlobLogStore>();

                services.AddAzureClients(builder =>
                {
                    builder.AddBlobServiceClient(blobStorageConnectionString);
                });
            }

            return services;
        }
    }
}
