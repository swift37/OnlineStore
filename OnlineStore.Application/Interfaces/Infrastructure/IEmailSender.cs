using OnlineStore.Application.Models;

namespace OnlineStore.Application.Interfaces.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(EmailRequest emailRequest, CancellationToken cancellation = default);
    }
}
