using OnlineStore.Application.DTOs.Base;

namespace OnlineStore.Application.DTOs.Subscriber
{
    public class UpdateSubscriberDTO : BaseDTO
    {
        public string? Email { get; set; }

        public DateTime SubscribeDate { get; set; }
    }
}
