using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(category => category.Id);
            builder.HasIndex(category => category.Id).IsUnique();
            builder.Property(category => category.Name).HasMaxLength(32);
            builder
                .HasOne(category => category.Parent)
                .WithMany(parent => parent.Subcategories)
                .HasForeignKey(category => category.ParentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
            builder.Navigation(category => category.Parent).AutoInclude();
        }
    }
}
