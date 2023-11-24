using OnlineStore.Application.DTOs.Base;

namespace OnlineStore.Application.DTOs.ContactRequest
{
    public class ContactRequestDTO : BaseDTO
    {
        public string? ContactName { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ResponseDate { get; set; }
    }
}
