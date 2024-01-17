using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Application.Models.Identity
{
    public class ConfirmEmailChangingRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
