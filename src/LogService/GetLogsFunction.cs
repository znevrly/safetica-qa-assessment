using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Threading;
using LogService.Services;
using System.Linq;
using LogService.Models;

namespace LogService
{
    /// <summary>
    /// Retrieves all logs.
    /// </summary>
    public class GetLogsFunction
    {
        private readonly ILogService _service;

        public GetLogsFunction(ILogService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [FunctionName("GetLogsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "logs")] HttpRequest request,
            CancellationToken cancellationToken)
        {
            var logs = await _service.GetLogsAsync(cancellationToken);
            return new OkObjectResult(logs.Select(x => x.Marshal()));
        }
    }
}
