using OnlineStore.Application.Models;

namespace OnlineStore.Application.Interfaces.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(EmailRequest emailRequest, CancellationToken cancellation = default);
    }
}
