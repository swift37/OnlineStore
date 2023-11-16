using Microsoft.Extensions.Options;
using OnlineStore.Application.Models;
using OnlineStore.Application.Interfaces.Infrastructure;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace OnlineStore.Application.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings) => 
            _emailSettings = emailSettings.Value;

        public async Task<bool> SendEmail(EmailRequest emailRequest, CancellationToken cancellation = default)
        {
            try
            {
                var email = new MimeMessage()
                {
                    Sender = MailboxAddress.Parse(_emailSettings.Email),
                    Subject = emailRequest.Subject
                };
                email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));


                var builder = new BodyBuilder();
                builder.HtmlBody = emailRequest.Body;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Connect(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);
                await smtp.SendAsync(email, cancellation);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
