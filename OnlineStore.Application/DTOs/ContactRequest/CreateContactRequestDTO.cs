namespace OnlineStore.Application.DTOs.ContactRequest
{
    public class CreateContactRequestDTO
    {
        public string? ContactName { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
