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
    public class DeleteLogFunction
    {
        private readonly ILogService _service;

        public DeleteLogFunction(ILogService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [FunctionName("DeleteCustomerFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "logs/{id}")] HttpRequest request,
            string id,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return new BadRequestResult();

            var deleted = await _service.DeleteLogIfExistsAsync(guid, cancellationToken);
            if (!deleted)
                return new NotFoundResult();

            return new NoContentResult();
        }
    }
}
