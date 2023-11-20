using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class Category : NamedEntity
    {
        public string? Description { get; set; }

        public int? RootId { get; set; }

        public Category? Root { get; set; }

        public int? ParentId { get; set; }

        public Category? Parent { get; set; }

        public bool IsRootCategory { get; set; }
    }
}
