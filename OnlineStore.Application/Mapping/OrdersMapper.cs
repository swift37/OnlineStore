﻿using OnlineStore.Application.DTOs;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class OrdersMapper
    {
        public static OrderDTO ToDTO(this Order order) => new OrderDTO
        {
            Id = order.Id,
            Items = order.Items.ToDTO().ToArray(),
            Status = order.Status,
            CreatedDate = order.CreatedDate,
            PayDate = order.PayDate,
            ShippedDate = order.ShippedDate,
            Email = order.Email,
            Phone = order.Phone,
            ShippingCost = order.ShippingCost,
            TrackingNumber = order.TrackingNumber,
            FirstName = order.FirstName,
            LastName = order.LastName,
            Country = order.Country,
            State = order.State,
            City = order.City,
            Postcode = order.Postcode,
            Apartment = order.Apartment,
            StreetAddress = order.StreetAddress,
            Notes = order.Notes
        };

        public static Order FromDTO(this OrderDTO order) => new Order
        {
            Id = order.Id,
            Items = order.Items.FromDTO().ToArray(),
            Status = order.Status,
            CreatedDate = order.CreatedDate,
            PayDate = order.PayDate,
            ShippedDate = order.ShippedDate,
            Email = order.Email,
            Phone = order.Phone,
            ShippingCost = order.ShippingCost,
            TrackingNumber = order.TrackingNumber,
            FirstName = order.FirstName,
            LastName = order.LastName,
            Country = order.Country,
            State = order.State,
            City = order.City,
            Postcode = order.Postcode,
            Apartment = order.Apartment,
            StreetAddress = order.StreetAddress,
            Notes = order.Notes
        };

        public static OrderItemDTO ToDTO(this OrderItem orderItem) => new OrderItemDTO
        {
            Id = orderItem.Id,
            Order = orderItem.Order?.ToDTO(),
            Product = orderItem.Product?.ToDTO(),
            Quantity = orderItem.Quantity,
            UnitPrice = orderItem.UnitPrice,
            Discount = orderItem.Discount
        };

        public static OrderItem FromDTO(this OrderItemDTO orderItem) => new OrderItem
        {
            Id = orderItem.Id,
            Order = orderItem.Order?.FromDTO(),
            Product = orderItem.Product?.FromDTO(),
            Quantity = orderItem.Quantity,
            UnitPrice = orderItem.UnitPrice,
            Discount = orderItem.Discount
        };

        public static IEnumerable<OrderDTO> ToDTO(this IEnumerable<Order> orders) => orders.Select(p => p.ToDTO());

        public static IEnumerable<Order> FromDTO(this IEnumerable<OrderDTO> orders) => orders.Select(p => p.FromDTO());

        public static IEnumerable<OrderItemDTO> ToDTO(this IEnumerable<OrderItem> orderItems) => orderItems.Select(p => p.ToDTO());

        public static IEnumerable<OrderItem> FromDTO(this IEnumerable<OrderItemDTO> orderItems) => orderItems.Select(p => p.FromDTO());
    }
}