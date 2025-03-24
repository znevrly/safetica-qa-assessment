namespace LogService.Api.Models
{
    /// <summary>
    /// Log of an email transaction.
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// The Id of the log.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// When the email was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Email address of the sender.
        /// </summary>
        public string Sender { get; }

        /// <summary>
        /// Email address of the recipients.
        /// </summary>
        public IEnumerable<string> Recipients { get; }

        /// <summary>
        /// Email subject.
        /// </summary>
        public string Subject { get; }

        public LogModel(Guid id, DateTimeOffset createdAt, string sender, IEnumerable<string> recipients, string subject)
        {
            Id = id;
            CreatedAt = createdAt;
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Recipients = recipients ?? throw new ArgumentNullException(nameof(recipients));
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }
    }
}
