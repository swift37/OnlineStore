using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(review => review.Id);
            builder.HasIndex(review => review.Id).IsUnique();
            builder.HasIndex(review => review.UserId);
            builder.Property(review => review.Title).HasMaxLength(32);
            builder.Property(review => review.UserId).IsRequired();
            builder.Property(review => review.Content).HasMaxLength(256);
            builder
                .HasOne(review => review.Product)
                .WithMany(product => product.Reviews)
                .HasForeignKey(review => review.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
