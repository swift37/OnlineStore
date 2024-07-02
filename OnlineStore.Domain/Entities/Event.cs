using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class Event : NamedEntity
    {
        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset FinishDate { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
