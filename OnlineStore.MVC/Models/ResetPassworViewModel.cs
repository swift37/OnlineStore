using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MVC.Models
{
    public class ResetPasswordViewModel
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
