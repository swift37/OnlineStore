using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class MenuItem : Entity
    {
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        public string? Image { get; set; }
    }
}
