using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class OrderConfiguration : BaseConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.HasIndex(order => order.UserId);
            builder.Property(order => order.ShippingCost).HasColumnType("decimal(18,2)");
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
