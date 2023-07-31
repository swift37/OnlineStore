using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class Order : Entity
    {
        public int CartId { get; set; }

        public Cart? Cart { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Postcode { get; set; }

        public string? Address { get; set; }
    }
}
