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
            builder.Ignore(filtersGroup => filtersGroup.MinPrice);
            builder.Ignore(filtersGroup => filtersGroup.MaxPrice);
            builder.Ignore(filtersGroup => filtersGroup.AppliedMinPrice);
            builder.Ignore(filtersGroup => filtersGroup.AppliedMaxPrice);
            builder
                .HasOne(filtersGroup => filtersGroup.Category)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasMany(filtersGroup => filtersGroup.SpecificationTypes)
                .WithMany();
            builder.Navigation(filtersGroup => filtersGroup.Category).AutoInclude();
            builder.Navigation(filtersGroup => filtersGroup.SpecificationTypes).AutoInclude();
        }
    }
}
