namespace LogService.Api.Models
{
    /// <summary>
    /// Model for the AddLog operation.
    /// </summary>
    public class LogAddModel
    {
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

        public LogAddModel(DateTimeOffset createdAt, string sender, IEnumerable<string> recipients, string subject)
        {
            CreatedAt = createdAt;
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Recipients = recipients ?? throw new ArgumentNullException(nameof(recipients));
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }
    }
}
