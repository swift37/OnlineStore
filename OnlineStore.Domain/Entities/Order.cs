﻿using OnlineStore.Domain.Base;
using OnlineStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Domain.Entities
{
    public class Order : Entity
    {
        [Required]
        public string? Number { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public OrderStatus Status { get; set; } = OrderStatus.ToPay;

        public DateTimeOffset CreatingDate { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset? PaymentDate { get; set; }

        public DateTimeOffset? ShippingDate { get; set; }

        public DateTimeOffset? DeliveryDate { get; set; }

        public int? PaymentMethodId { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }

        public string? PaymentSession { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public decimal Total { get; set; }

        public int? ShippingMethodId { get; set; }

        public ShippingMethod? ShippingMethod { get; set; }

        public string? TrackingNumber { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Postcode { get; set; }

        public string? StreetAddress { get; set; }

        public string? Apartment { get; set; }

        public string? Notes { get; set; }
    }

    public class OrderItem : Entity
    {
        public int OrderId { get; set; }

        public Order? Order { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }
    }
}
