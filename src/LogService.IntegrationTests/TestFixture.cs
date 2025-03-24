using Azure.Storage.Blobs;
using LogService.Services;
using LogService.Services.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace LogService.IntegrationTests;

public class TestFixture : IDisposable
{
    private readonly IConfiguration _configuration;
    
    public IServiceProvider Services { get; }

    public TestFixture()
    {
        var environment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Development";
        
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
            
        var services = new ServiceCollection();
        
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Debug);
        });

        services.AddSingleton<BlobServiceClient>(_ => 
            new BlobServiceClient("UseDevelopmentStorage=true"));
        
        services.AddScoped<ILogStore, BlobLogStore>();
        services.AddScoped<ILogService, LogService.Services.LogService>();

        Services = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        if (Services is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
} 