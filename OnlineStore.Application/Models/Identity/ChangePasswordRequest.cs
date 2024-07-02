using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Application.Models.Identity
{
    public class ChangePasswordRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string NewPasswordConfirmation { get; set; }
    }
}
