using LogService.Api;
using LogService.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Configure
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Set up
var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration);
services.AddLogClient(x => x.BindConfiguration("LogClient"));
services.AddLogging(x => x.AddConsole());

// Create services
var provider = services.BuildServiceProvider();
var client = provider.GetRequiredService<ILogClient>();
var logger = provider.GetRequiredService<ILogger<Program>>();

// Add a log
var log = await client.AddLogAsync(new LogAddModel(DateTimeOffset.Now, "sender@test.com", new[] { "recipient@test.com" }, "Test"), default);
logger.LogInformation("Added log {LogId}", log.Id);

// Retrieve all logs
var logs = await client.GetLogsAsync(default);
logger.LogInformation("Received logs: {LogIds}", logs.Select(x => x.Id));

// Delete log
await client.DeleteLogAsync(log.Id, default);
logger.LogInformation("Deleted log {LogId}", log.Id);

// Retrieve all logs
var logsAfterDeletion = await client.GetLogsAsync(default);
logger.LogInformation("Received logs: {LogIds}", logsAfterDeletion.Select(x => x.Id));