using Microsoft.EntityFrameworkCore;
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
            builder.Navigation(wishlist => wishlist.Items).AutoInclude();
        }
    }

    public class WishlistItemConfiguration : BaseConfiguration<WishlistItem>
    {
        public override void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            base.Configure(builder);
            builder
                .HasOne(wishlistItem => wishlistItem.Wishlist)
                .WithMany(wishlist => wishlist.Items)
                .HasForeignKey(wishlistItem => wishlistItem.WishlistId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder
                .HasOne(wishlistItem => wishlistItem.Product)
                .WithMany()
                .HasForeignKey(wishlistItem => wishlistItem.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(wishlistItem => wishlistItem.Product).AutoInclude();
        }
    }
}
