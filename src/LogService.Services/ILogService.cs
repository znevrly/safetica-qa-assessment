using LogService.Services.Models;

namespace LogService.Services
{
    /// <summary>
    /// Handles log operations.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Stores a new log.
        /// </summary>
        /// <param name="log">the log</param>
        Task AddLogAsync(Log log, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all stored logs.
        /// </summary>
        /// <returns>the logs</returns>
        Task<IEnumerable<Log>> GetLogsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a log.
        /// </summary>
        /// <param name="id">ID of the log to delete</param>
        /// <returns>true if log was found and deleted, false if not found</returns>
        Task<bool> DeleteLogIfExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}