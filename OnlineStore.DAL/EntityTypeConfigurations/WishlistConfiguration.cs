using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(wishlist => wishlist.Id);
            builder.HasIndex(wishlist => wishlist.Id).IsUnique();
            builder.HasIndex(wishlist => wishlist.UserId);
            builder.Property(wishlist => wishlist.UserId).IsRequired();
            builder
                .HasMany(wishlist => wishlist.Products)
                .WithMany();
            builder.Navigation(wishlist => wishlist.Products).AutoInclude();
        }
    }
}
