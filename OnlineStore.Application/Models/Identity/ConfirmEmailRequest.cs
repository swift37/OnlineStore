using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Application.Models.Identity
{
    public class ConfirmEmailRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
