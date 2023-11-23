namespace OnlineStore.Application.DTOs.Subscriber
{
    public class CreateSubscriberDTO
    {
        public string? Email { get; set; }

        public DateTime SubscribeDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
