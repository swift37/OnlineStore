using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class Event : NamedEntity
    {
        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
