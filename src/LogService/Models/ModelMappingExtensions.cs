using LogService.Api.Models;
using LogService.Services.Models;

namespace LogService.Models
{
    internal static class ModelMappingExtensions
    {
        public static LogModel Marshal(this Log x) =>
            new(x.Id, x.CreatedAt, x.Sender, x.Recipients, x.Subject);
    }
}
