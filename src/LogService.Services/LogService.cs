using LogService.Services.Models;
using LogService.Services.Storage;
using Microsoft.Extensions.Logging;

namespace LogService.Services
{
    public class LogService : ILogService
    {
        private ILogStore _store;
        private ILogger<LogService> _logger;

        public LogService(ILogStore store, ILogger<LogService> logger)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddLogAsync(Log log, CancellationToken cancellationToken)
        {
            await _store.AddLogAsync(log, cancellationToken);
            _logger.LogInformation("Added log {LogId}", log.Id);
        }

        public Task<IEnumerable<Log>> GetLogsAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all logs");
            return _store.GetLogsAsync(cancellationToken);
        }

        public Task<bool> DeleteLogIfExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleteing log {LogId}", id);
            return _store.DeleteLogIfExistsAsync(id, cancellationToken);
        }
    }
}