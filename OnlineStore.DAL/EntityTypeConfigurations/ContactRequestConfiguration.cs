using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain;

namespace OnlineStore.DAL.EntityTypeConfigurations
{
    public class ContactRequestConfiguration : IEntityTypeConfiguration<ContactRequest>
    {
        public void Configure(EntityTypeBuilder<ContactRequest> builder)
        {
            builder.HasKey(contactRequest => contactRequest.Id);
            builder.HasIndex(contactRequest => contactRequest.Id).IsUnique();
            builder.Property(contactRequest => contactRequest.ContactName).HasMaxLength(32);
            builder.Property(contactRequest => contactRequest.Message).HasMaxLength(256);
        }
    }
}
