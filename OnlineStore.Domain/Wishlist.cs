using OnlineStore.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain
{
    public class Wishlist : Entity
    {
        public ApplicationUser? User { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime LastChangeDate { get; set; } = DateTime.Now;

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        [NotMapped]
        public decimal SubtotalPrice => Products.Sum(p => p.UnitPrice);

        [NotMapped]
        public decimal TotalDiscount => Products.Sum(p => p.Discount);

        [NotMapped]
        public decimal TotalPrice => SubtotalPrice - TotalDiscount;

        [NotMapped]
        public int TotalQuantity => Products.Count();
    }
}
