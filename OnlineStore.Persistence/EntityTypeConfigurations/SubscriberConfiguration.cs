﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class SubscriberConfiguration : BaseConfiguration<Subscriber>
    {
        public override void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            base.Configure(builder);
            builder.HasIndex(s => s.Email).IsUnique();
            builder.Property(s => s.Email).IsRequired();
        }
    }
}
