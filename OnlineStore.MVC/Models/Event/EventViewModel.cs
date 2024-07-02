namespace OnlineStore.MVC.Models.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset FinishDate { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
