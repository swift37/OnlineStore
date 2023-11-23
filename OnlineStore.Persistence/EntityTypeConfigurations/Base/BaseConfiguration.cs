using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Base;

namespace OnlineStore.DAL.EntityTypeConfigurations.Base
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.HasIndex(entity => entity.Id).IsUnique();
        }
    }
}
