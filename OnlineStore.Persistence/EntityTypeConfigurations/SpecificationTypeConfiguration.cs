using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class SpecificationTypeConfiguration : BaseConfiguration<SpecificationType>
    {
        public override void Configure(EntityTypeBuilder<SpecificationType> builder)
        {
            base.Configure(builder);
            builder.Property(specificationType => specificationType.Name).HasMaxLength(32);
            builder.Property(specificationType => specificationType.DisplayName).HasMaxLength(32);
            builder
                .HasMany(specificationType => specificationType.Values)
                .WithOne(specification => specification.SpecificationType)
                .HasForeignKey(specification => specification.SpecificationTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.Navigation(specificationType => specificationType.Values).AutoInclude();
        }
    }
}
