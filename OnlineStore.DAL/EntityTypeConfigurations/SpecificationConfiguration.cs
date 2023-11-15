using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class SpecificationConfiguration : IEntityTypeConfiguration<Specification>
    {
        public void Configure(EntityTypeBuilder<Specification> builder)
        {
            builder.HasKey(specification => specification.Id);
            builder.HasIndex(specification => specification.Id).IsUnique();
            builder.Property(specification => specification.Name).HasMaxLength(32);
            builder.Property(specification => specification.Value).HasMaxLength(32);
        }
    }
}
