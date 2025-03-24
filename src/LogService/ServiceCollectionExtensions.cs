using LogService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogService
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all required services to the service collection.
        /// </summary>
        /// <param name="services">the service collection</param>
        /// <param name="configuration">the host configuration</param>
        public static IServiceCollection AddFunctionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogServices(configuration.GetValue<string>("BlobStorageConnectionString"));

            return services;
        }
    }
}