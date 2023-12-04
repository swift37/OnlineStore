namespace OnlineStore.MVC.Models.ContactRequest
{
    public class ContactRequestViewModel
    {
        public int Id { get; set; }

        public string? ContactName { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public DateTimeOffset? ResponseDate { get; set; }
    }
}
