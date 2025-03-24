using LogService.Api.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LogService.Api
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all required services to the service collection.
        /// </summary>
        /// <param name="services">the service collection</param>
        /// <param name="configure">configures required options</param>
        public static IServiceCollection AddLogClient(
            this IServiceCollection services, 
            Action<OptionsBuilder<LogClientOptions>>? configure = null)
        {
            configure?.Invoke(services.AddOptions<LogClientOptions>());
            services.AddHttpClient<ILogClient, LogClient>((s, x) =>
            {
                var options = s.GetRequiredService<IOptions<LogClientOptions>>().Value;
                if (string.IsNullOrEmpty(options.BaseApiUri))
                    throw new InvalidOperationException("Base API URI not specified");

                x.BaseAddress = new Uri(options.BaseApiUri?.TrimEnd('/') + "/");
            });

            return services;
        }
    }
}
