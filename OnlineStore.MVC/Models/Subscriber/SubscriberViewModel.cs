namespace OnlineStore.MVC.Models.Subscriber
{
    public class SubscriberViewModel
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public DateTimeOffset SubscribeDate { get; set; }

        public bool IsActive { get; set; }
    }
}
