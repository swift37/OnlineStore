using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class CouponConfiguration : BaseConfiguration<Coupon>
    {
        public override void Configure(EntityTypeBuilder<Coupon> builder)
        {
            base.Configure(builder);
            builder.HasIndex(coupon => coupon.Number).IsUnique();
            builder.Property(coupon => coupon.Number).HasMaxLength(16).IsRequired();
            builder.Property(coupon => coupon.DiscountSize).HasColumnType("decimal(18,2)");
        }
    }
}
