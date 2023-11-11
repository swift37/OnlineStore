using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class Specification : NamedEntity
    {
        public string? Value { get; set; }

        public bool IsMain { get; set; }
    }
}
