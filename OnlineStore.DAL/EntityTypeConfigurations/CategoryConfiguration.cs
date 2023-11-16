using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class CategoryConfiguration : BaseConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
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
