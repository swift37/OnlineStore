using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Application.Models.Identity
{
    public class ResetPasswordRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string NewPasswordConfirmation { get; set; }
    }
}
