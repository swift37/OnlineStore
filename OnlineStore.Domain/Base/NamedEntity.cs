using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Domain.Base
{
    public abstract class NamedEntity : Entity
    {
        [Required]
        public string? Name { get; set; }
    }
}
