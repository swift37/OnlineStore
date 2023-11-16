using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class SubscriberConfiguration : BaseConfiguration<Subscriber>
    {
        public override void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            base.Configure(builder);
        }
    }
}
