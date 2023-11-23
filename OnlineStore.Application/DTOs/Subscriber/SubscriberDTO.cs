using OnlineStore.Application.DTOs.Base;

namespace OnlineStore.Application.DTOs.Subscriber
{
    public class SubscriberDTO : BaseDTO
    {
        public string? Email { get; set; }

        public DateTime SubscribeDate { get; set; }

        public bool IsActive { get; set; }
    }
}
