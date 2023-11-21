
namespace OnlineStore.Application.DTOs.Event
{
    public class CreateEventDTO
    {
        public string? Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
