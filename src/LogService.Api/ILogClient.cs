using LogService.Api.Models;

namespace LogService.Api
{
    /// <summary>
    /// API wrapper for the log service.
    /// </summary>
    public interface ILogClient
    {
        /// <summary>
        /// Stores a new log.
        /// </summary>
        /// <param name="log">logged data</param>
        /// <returns>the log that was added</returns>
        Task<LogModel> AddLogAsync(LogAddModel log, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all logs.
        /// </summary>
        /// <returns>the logs</returns>
        Task<IReadOnlyCollection<LogModel>> GetLogsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Stores a new log.
        /// </summary>
        /// <param name="id">ID of the log to delete</param>
        Task DeleteLogAsync(Guid id, CancellationToken cancellationToken);
    }
}