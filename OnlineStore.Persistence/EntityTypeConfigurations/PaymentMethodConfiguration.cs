using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.EntityTypeConfigurations
{
    public class PaymentMethodConfiguration : BaseConfiguration<PaymentMethod>
    {
        public override void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            base.Configure(builder);
            builder.HasIndex(paymentMethod => paymentMethod.Name).IsUnique();
            builder.Property(paymentMethod => paymentMethod.Name).HasMaxLength(32);
            builder.Property(paymentMethod => paymentMethod.DisplayName).HasMaxLength(32);
        }
    }
}
