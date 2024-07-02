using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MVC.Models
{
    public class ChangePasswordViewModel
    {
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
