using OnlineStore.Application.DTOs.Base;

namespace OnlineStore.Application.DTOs.Event
{
    public class EventDTO : BaseDTO
    {
        public string? Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
