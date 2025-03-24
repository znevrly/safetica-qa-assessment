using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using LogService.Api.Models;
using System.Threading;
using LogService.Services;
using LogService.Services.Models;

namespace LogService
{
    /// <summary>
    /// Stores a new log.
    /// </summary>
    public class AddLogFunction
    {
        private readonly ILogService _service;

        public AddLogFunction(ILogService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [FunctionName("AddLogFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "logs")] HttpRequest request,
            CancellationToken cancellationToken)
        {
            var payload = await JsonSerializer.DeserializeAsync<LogAddModel>(request.Body, cancellationToken: cancellationToken);

            var log = new Log(Guid.NewGuid(), payload.CreatedAt, payload.Sender, payload.Recipients, payload.Subject);
            await _service.AddLogAsync(log, cancellationToken);

            return new CreatedResult($"logs/{log.Id}", log);
        }
    }
}
