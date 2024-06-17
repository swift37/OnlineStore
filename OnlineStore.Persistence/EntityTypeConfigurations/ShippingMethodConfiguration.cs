using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.EntityTypeConfigurations
{
    public class ShippingMethodConfiguration : BaseConfiguration<ShippingMethod>
    {
        public override void Configure(EntityTypeBuilder<ShippingMethod> builder)
        {
            base.Configure(builder);
            builder.HasIndex(shippingMethod => shippingMethod.Name).IsUnique();
            builder.Property(shippingMethod => shippingMethod.Name).HasMaxLength(32);
            builder.Property(shippingMethod => shippingMethod.DisplayName).HasMaxLength(32);
            builder.Property(shippingMethod => shippingMethod.Price).HasColumnType("decimal(18,2)");
        }
    }
}
