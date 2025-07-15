
using MimeKit;

namespace EmailNotification.EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; private init; }

        public string Subject { get; private init; }
        public string Content { get; private init; }

        public EmailContentFormart Format { get; private init; }
        public Message(IEnumerable<string> to, string subject, string content
                        , EmailContentFormart format = EmailContentFormart.Text)
        {
            To = to.Select(c => new MailboxAddress(c, c)).ToList();
            Subject = subject;
            Content = content;
            Format = format;
        }

    }
}
