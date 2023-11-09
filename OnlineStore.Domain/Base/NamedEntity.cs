using OnlineStore.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Domain.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Required]
        public string? Name { get; set; }
    }
}
