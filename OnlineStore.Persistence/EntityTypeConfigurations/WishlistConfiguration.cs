using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class WishlistConfiguration : BaseConfiguration<Wishlist>
    {
        public override void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            base.Configure(builder);
            builder.HasIndex(wishlist => wishlist.UserId);
            builder.Property(wishlist => wishlist.UserId).IsRequired();
            builder
                .HasMany(wishlist => wishlist.Products)
                .WithMany();
            builder.Navigation(wishlist => wishlist.Products).AutoInclude();
        }
    }
}
