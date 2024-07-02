using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Application.Models.Identity
{
    public class RefreshRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
