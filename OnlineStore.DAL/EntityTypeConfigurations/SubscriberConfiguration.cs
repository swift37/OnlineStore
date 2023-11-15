using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.HasKey(subscriber => subscriber.Id);
            builder.HasIndex(subscriber => subscriber.Id).IsUnique();
        }
    }
}
