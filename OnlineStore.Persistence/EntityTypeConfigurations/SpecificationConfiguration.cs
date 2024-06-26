﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.EntityTypeConfigurations
{
    public class SpecificationConfiguration : BaseConfiguration<Specification>
    {
        public override void Configure(EntityTypeBuilder<Specification> builder)
        {
            base.Configure(builder);
            builder.Property(specification => specification.Value).HasMaxLength(32);
            builder.Ignore(specification => specification.ProductsCount);
            builder
                .HasOne(specification => specification.SpecificationType)
                .WithMany(specificationType => specificationType.Values)
                .HasForeignKey(specification => specification.SpecificationTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
