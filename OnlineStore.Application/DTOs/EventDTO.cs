namespace OnlineStore.Application.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
