namespace OnlineStore.Application.DTOs
{
    public class ContactRequestDTO
    {
        public int Id {  get; set; }

        public string? ContactName { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ResponseDate { get; set; }
    }
}
