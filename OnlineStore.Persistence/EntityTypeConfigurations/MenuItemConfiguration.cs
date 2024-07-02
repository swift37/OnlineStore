using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.EntityTypeConfigurations
{
    public class MenuItemConfiguration : BaseConfiguration<MenuItem>
    {
        public override void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            base.Configure(builder);
            builder.Property(menuItem => menuItem.Name).HasMaxLength(32);
            builder
                .HasOne(menuItem => menuItem.Category)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
            builder.Navigation(menuItem => menuItem.NestedItems).AutoInclude();
        }
    }

    public class NestedMenuItemConfiguration : BaseConfiguration<NestedMenuItem>
    {
        public override void Configure(EntityTypeBuilder<NestedMenuItem> builder)
        {
            base.Configure(builder);
            builder.Property(nestedMenuItem => nestedMenuItem.Name).HasMaxLength(32);
            builder
                .HasOne(nestedMenuItem => nestedMenuItem.Parent)
                .WithMany(menuItem => menuItem.NestedItems)
                .HasForeignKey(nestedMenuItem => nestedMenuItem.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder
                .HasMany(nestedMenuItem => nestedMenuItem.Categories)
                .WithMany();
            builder.Navigation(nestedMenuItem => nestedMenuItem.Categories).AutoInclude();
        }
    }
}
