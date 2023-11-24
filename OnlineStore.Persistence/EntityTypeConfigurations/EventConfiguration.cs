using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class EventConfiguration : BaseConfiguration<Event>
    {
        public override void Configure(EntityTypeBuilder<Event> builder)
        {
            base.Configure(builder);
            builder.Property(@event => @event.Name).HasMaxLength(32);
        }
    }
}
