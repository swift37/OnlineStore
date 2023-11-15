using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);
            builder.HasIndex(product => product.Id).IsUnique();
            builder.Property(product => product.Name).HasMaxLength(32);
            builder.Property(product => product.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(product => product.Discount).HasColumnType("decimal(18,2)");
            builder
                .HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasMany(product => product.Specifications)
                .WithMany();
            builder.Navigation(product => product.Category).AutoInclude();
            builder.Navigation(product => product.Specifications).AutoInclude();
        }
    }
}
