using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class Subscriber : Entity
    {
        public string? Email { get; set; }

        public DateTime SubscribeDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
