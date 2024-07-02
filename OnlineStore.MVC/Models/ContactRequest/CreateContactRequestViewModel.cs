namespace OnlineStore.MVC.Models.ContactRequest
{
    public class CreateContactRequestViewModel
    {
        public string? ContactName { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }

        public bool IsConsidered { get; set; }
    }
}
