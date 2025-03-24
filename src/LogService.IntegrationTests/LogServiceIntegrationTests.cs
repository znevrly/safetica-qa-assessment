using FluentAssertions;
using LogService.Services;
using LogService.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LogService.IntegrationTests;

public class LogServiceIntegrationTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;
    private readonly ILogService _logService;

    public LogServiceIntegrationTests(TestFixture fixture)
    {
        _fixture = fixture;
        _logService = _fixture.Services.GetRequiredService<ILogService>();
    }

    [Fact]
    public async Task AddLog_ShouldStoreLogInStorage()
    {
        // Arrange
        var log = new Log(
            Guid.NewGuid(),
            DateTimeOffset.UtcNow,
            "znevrly@gmail.com",
            new[] { "recipient@example.org" },
            "Integration Test Subject"
        );

        // Act
        await _logService.AddLogAsync(log, CancellationToken.None);

        // Assert
        var logs = await _logService.GetLogsAsync(CancellationToken.None);
        logs.Should().Contain(l => l.Id == log.Id);
        var storedLog = logs.First(l => l.Id == log.Id);
        storedLog.Should().BeEquivalentTo(log);
    }

    [Fact]
    public async Task GetLogs_ShouldReturnAllStoredLogs()
    {
        // Arrange
        var log1 = new Log(Guid.NewGuid(), DateTimeOffset.UtcNow, "test1@example.org", new[] { "recipient1@example.org" }, "Subject 1");
        var log2 = new Log(Guid.NewGuid(), DateTimeOffset.UtcNow, "test2@example.org", new[] { "recipient2@example.org" }, "Subject 2");

        await _logService.AddLogAsync(log1, CancellationToken.None);
        await _logService.AddLogAsync(log2, CancellationToken.None);

        // Act
        var retrievedLogs = await _logService.GetLogsAsync(CancellationToken.None);

        // Assert
        retrievedLogs.Should().HaveCountGreaterThanOrEqualTo(2);
        retrievedLogs.Should().Contain(l => l.Id == log1.Id);
        retrievedLogs.Should().Contain(l => l.Id == log2.Id);
    }

    [Fact]
    public async Task DeleteLog_ShouldRemoveLogFromStorage()
    {
        // Arrange
        var log = new Log(
            Guid.NewGuid(),
            DateTimeOffset.UtcNow,
            "znevrly@gmail.com",
            new[] { "recipient@example.org" },
            "Integration Test Subject"
        );
        await _logService.AddLogAsync(log, CancellationToken.None);

        // Act
        var result = await _logService.DeleteLogIfExistsAsync(log.Id, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        var logs = await _logService.GetLogsAsync(CancellationToken.None);
        logs.Should().NotContain(l => l.Id == log.Id);
    }

    [Fact]
    public async Task DeleteNonExistentLog_ShouldReturnFalse()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _logService.DeleteLogIfExistsAsync(nonExistentId, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task AddLog_ShouldNotAllowDuplicateIds()
    {
        // Arrange
        var log = new Log(
            Guid.NewGuid(),
            DateTimeOffset.UtcNow,
            "znevrly@gmail.com",
            new[] { "recipient@example.org" },
            "Integration Test Subject"
        );
        await _logService.AddLogAsync(log, CancellationToken.None);

        // Act & Assert
        await _logService.Invoking(s => s.AddLogAsync(log, CancellationToken.None))
            .Should().ThrowAsync<InvalidOperationException>();
    }
} 