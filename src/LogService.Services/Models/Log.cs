namespace LogService.Services.Models
{
    public class Log
    {
        public Guid Id { get; }
        public DateTimeOffset CreatedAt { get; }
        public string Sender { get; }
        public IEnumerable<string> Recipients { get; }
        public string Subject { get; }

        public Log(Guid id, DateTimeOffset createdAt, string sender, IEnumerable<string> recipients, string subject)
        {
            Id = id;
            CreatedAt = createdAt;
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Recipients = recipients ?? throw new ArgumentNullException(nameof(recipients));
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }
    }
}
