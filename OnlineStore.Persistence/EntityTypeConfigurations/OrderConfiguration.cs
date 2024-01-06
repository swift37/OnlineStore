using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class OrderConfiguration : BaseConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.HasIndex(order => order.UserId);
            builder.HasIndex(order => order.Number);
            builder.HasIndex(order => order.TrackingNumber);
            builder.Property(order => order.Number).HasMaxLength(16).IsRequired();
            builder.Property(order => order.UserId).IsRequired();
            builder.Property(order => order.FirstName).HasMaxLength(32);
            builder.Property(order => order.LastName).HasMaxLength(32);
            builder.Property(order => order.Phone).HasMaxLength(16);
            builder.Property(order => order.Total).HasColumnType("decimal(18,2)");
            builder.Property(order => order.ShippingCost).HasColumnType("decimal(18,2)");
            builder.Property(order => order.TrackingNumber).HasMaxLength(32);
            builder.Property(order => order.Country).HasMaxLength(32);
            builder.Property(order => order.City).HasMaxLength(32);
            builder.Property(order => order.State).HasMaxLength(32);
            builder.Property(order => order.Postcode).HasMaxLength(10);
            builder.Property(order => order.StreetAddress).HasMaxLength(32);
            builder.Property(order => order.Apartment).HasMaxLength(8);
            builder.Property(order => order.Notes).HasMaxLength(64);
            builder.Navigation(order => order.Items).AutoInclude();
        }
    }

    public class OrderItemConfiguration : BaseConfiguration<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);
            builder.Property(orderItem => orderItem.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(orderItem => orderItem.Discount).HasColumnType("decimal(18,2)");
            builder
                .HasOne(orderItem => orderItem.Order)
                .WithMany(order => order.Items)
                .HasForeignKey(orderItem => orderItem.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder
                .HasOne(orderItem => orderItem.Product)
                .WithMany()
                .HasForeignKey(orderItem => orderItem.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(orderItem => orderItem.Product).AutoInclude();
        }
    }
}
