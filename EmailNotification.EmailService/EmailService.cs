using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json;

namespace EmailNotification.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailConfiguration> options, ILogger<EmailService> logger)
        {
            this._emailConfig = options.Value;
            this._logger = logger;
        }
        public async Task<bool> SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            return await SendAsync(mailMessage);
        }

        private async Task<bool> SendAsync(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                await client.SendAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
            finally
            {
                await client.DisconnectAsync(true);
                //client.Dispose();
            }
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var format = message.Format switch
            {
                EmailContentFormart.Text => MimeKit.Text.TextFormat.Text,
                EmailContentFormart.Html => MimeKit.Text.TextFormat.Html,
                _ => MimeKit.Text.TextFormat.Text,
            };


            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(format) { Text = message.Content };
            return emailMessage;
        }
    }
}
