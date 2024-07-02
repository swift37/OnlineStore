using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class ProductConfiguration : BaseConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.Property(product => product.Name).HasMaxLength(32);
            builder.Property(product => product.UnitCost).HasColumnType("decimal(18,2)");
            builder.Property(product => product.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(product => product.Discount).HasColumnType("decimal(18,2)");
            builder.Property(product => product.Manufacturer).HasMaxLength(32);
            builder.Property(product => product.ManufacturersCode).HasMaxLength(32);
            builder.Property(product => product.StoreCode).HasMaxLength(32);
            builder
                .HasOne(product => product.Category)
                .WithMany()
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasMany(product => product.Specifications)
                .WithMany(specification => specification.Products);
            builder
                .HasMany(product => product.Tags)
                .WithMany(tag => tag.Products);
            builder.Navigation(product => product.Specifications).AutoInclude();
            builder.Navigation(product => product.Tags).AutoInclude();
        }
    }
}
