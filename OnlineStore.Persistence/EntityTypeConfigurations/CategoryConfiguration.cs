﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class CategoryConfiguration : BaseConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.Property(category => category.Name).HasMaxLength(32);
            builder.Property(category => category.Description).HasMaxLength(128);
            builder
                .HasOne(category => category.Root)
                .WithMany()
                .HasForeignKey(category => category.RootId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
            builder
                .HasMany(category => category.ChildCategories)
                .WithOne(category => category.Parent)
                .HasForeignKey(category => category.ParentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
