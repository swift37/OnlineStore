using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.EntityTypeConfigurations
{
    public class ProductTagConfiguration : BaseConfiguration<ProductTag>
    {
        public override void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            base.Configure(builder);
            builder.HasIndex(productTag => productTag.Name).IsUnique();
            builder.Property(productTag => productTag.Name).HasMaxLength(32);
            builder.Property(productTag => productTag.DisplayName).HasMaxLength(32);
            builder.Property(productTag => productTag.ColorHex).HasMaxLength(7);
        }
    }
}
