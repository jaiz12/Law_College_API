using DTO.Models.Auth;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace API.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string bodyHtml);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<bool> SendEmailAsync(
            string to,
            string subject,
            string bodyHtml)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(
                    _settings.SenderName,
                    _settings.Sender));

                message.To.Add(MailboxAddress.Parse(to));

                message.Subject = subject;

                message.Body = new TextPart("html")
                {
                    Text = bodyHtml
                };

                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(
                    _settings.MailServer,
                    _settings.MailPort,
                    SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(
                    _settings.Sender,
                    _settings.Password);

                await smtp.SendAsync(message);

                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}