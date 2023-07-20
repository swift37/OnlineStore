using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Domain.Base
{
    public class NamedEntity : Entity
    {
        [Required]
        public string? Name { get; set; }
    }
}
