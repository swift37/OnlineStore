using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class Wishlist : Entity
    {
        public ApplicationUser? User { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime LastChangeDate { get; set; } = DateTime.Now;

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public int ProductsQuantity => Products.Count;
    }
}
