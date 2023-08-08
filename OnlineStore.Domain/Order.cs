using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStore.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain
{
    public class Order : Entity
    {
        public int CartId { get; set; }

        public Cart? Cart { get; set; }

        //public int UserId { get; set; }

        public ApplicationUser? User { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.NotPaid;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Postcode { get; set; }

        public string? StreetAddress { get; set; }

        public string? Apartment { get; set; }
    }

    public enum OrderStatus
    {
         NotPaid, Paid, Processed, Completed, Canceled
    }
}
