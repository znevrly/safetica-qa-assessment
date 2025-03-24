using Azure.Storage.Blobs;
using LogService.Services;
using LogService.Services.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LogService.IntegrationTests;

public class TestFixture : IDisposable
{
    public IServiceProvider Services { get; }

    public TestFixture()
    {
        var services = new ServiceCollection();
        
        // Configure logging
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Debug);
        });

        // Add Log Service and its dependencies
        services.AddSingleton<BlobServiceClient>(_ => 
            new BlobServiceClient("UseDevelopmentStorage=true"));
        services.AddSingleton<ILogStore, BlobLogStore>();
        services.AddSingleton<ILogService, LogService.Services.LogService>();

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