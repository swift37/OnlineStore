using OnlineStore.Domain;

namespace OnlineStore.Application.DTOs
{
    public class WishlistDTO
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastChangeDate { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
