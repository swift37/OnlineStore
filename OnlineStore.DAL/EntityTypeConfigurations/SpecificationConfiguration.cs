using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class SpecificationConfiguration : BaseConfiguration<Specification>
    {
        public override void Configure(EntityTypeBuilder<Specification> builder)
        {
            base.Configure(builder);
            builder.Property(specification => specification.Name).HasMaxLength(32);
            builder.Property(specification => specification.Value).HasMaxLength(32);
        }
    }
}
