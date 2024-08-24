
namespace EmailNotification.EmailService
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Message message);
    }
}
