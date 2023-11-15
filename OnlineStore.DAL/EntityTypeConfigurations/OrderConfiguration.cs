using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(order => order.Id);
            builder.HasIndex(order => order.Id).IsUnique();
            builder.HasIndex(order => order.UserId);
            builder.Property(order => order.ShippingCost).HasColumnType("decimal(18,2)");
            builder.Property(order => order.Notes).HasMaxLength(64);
            builder.Navigation(order => order.Items).AutoInclude();
        }
    }

    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(orderItem => orderItem.Id);
            builder.HasIndex(orderItem => orderItem.Id).IsUnique();
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
