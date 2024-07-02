using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Application.Models.Identity
{
    public class ChangeEmailRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
