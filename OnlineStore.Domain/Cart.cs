using OnlineStore.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain
{
    public class Cart : Entity
    {
        public CartStatus Status { get; set; } = CartStatus.Active;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime? PayDate { get; set; }

        public IEnumerable<CartItem>? CartItems { get; set; }

        [NotMapped]  
        public decimal TotalPrice => CartItems?.Sum(i => i.Product?.UnitPrice * i.Quantity) ?? 0;

        [NotMapped]
        public int TotalQuantity => CartItems?.Sum(i => i.Quantity) ?? 0;
    }

    public enum CartStatus
    {
        Completed, Active
    }
}
