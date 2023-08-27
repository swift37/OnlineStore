using OnlineStore.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain
{
    public class Cart : Entity
    {
        //public int UserId { get; set; }

        public ApplicationUser? User { get; set; }

        public CartStatus Status { get; set; } = CartStatus.Active;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime? PayDate { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();

        [NotMapped]
        public decimal SubtotalPrice => CartItems.Sum(i => i.Product?.UnitPrice * i.Quantity) ?? default;

        [NotMapped]
        public decimal TotalDiscount => CartItems.Sum(i => i.Product?.Discount * i.Quantity) ?? default;

        [NotMapped]
        public decimal TotalPrice => SubtotalPrice - TotalDiscount;

        [NotMapped]
        public int TotalQuantity => CartItems.Sum(i => i.Quantity);
    }

    public enum CartStatus
    {
        Completed, Active
    }
}
