using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class ContactRequest : Entity
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime ResponseDate { get; set; }
    }
}
