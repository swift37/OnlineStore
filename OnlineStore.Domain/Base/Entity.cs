using OnlineStore.Interfaces.Entities;

namespace OnlineStore.Domain.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
