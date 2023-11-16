using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DAL.EntityTypeConfigurations.Base;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class ContactRequestConfiguration : BaseConfiguration<ContactRequest>
    {
        public override void Configure(EntityTypeBuilder<ContactRequest> builder)
        {
            base.Configure(builder);
            builder.Property(contactRequest => contactRequest.ContactName).HasMaxLength(32);
            builder.Property(contactRequest => contactRequest.Message).HasMaxLength(256);
        }
    }
}
