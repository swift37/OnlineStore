using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.EntityTypeConfigurations
{
    public class FiltersGroupConfiguration : BaseConfiguration<FiltersGroup>
    {
        public override void Configure(EntityTypeBuilder<FiltersGroup> builder)
        {
            base.Configure(builder);
            builder
                .HasOne(filtersGroup => filtersGroup.Category)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasMany(filtersGroup => filtersGroup.Specifications)
                .WithMany();
            builder.Navigation(filtersGroup => filtersGroup.Specifications).AutoInclude();
        }
    }
}
