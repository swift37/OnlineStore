using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(coupon => coupon.Id);
            builder.HasIndex(coupon => coupon.Id).IsUnique();
            builder.Property(coupon => coupon.DiscountSize).HasColumnType("decimal(18,2)");
        }
    }
}
