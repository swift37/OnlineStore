using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(@event => @event.Id);
            builder.HasIndex(@event => @event.Id).IsUnique();
            builder.Property(@event => @event.Name).HasMaxLength(32);
        }
    }
}
