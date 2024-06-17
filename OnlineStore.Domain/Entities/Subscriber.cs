using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class Subscriber : Entity
    {
        public string? Email { get; set; }

        public DateTimeOffset SubscribeDate { get; set; } = DateTimeOffset.Now;

        public bool IsActive { get; set; } = true;
    }
}
