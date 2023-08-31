using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class Subscriber : Entity
    {
        public string? Email { get; set; }

        public DateTime SubscribeDate { get; set; } = DateTime.Now;
    }
}
