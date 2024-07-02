using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MVC.Models
{
    public class ResetPasswordRequestViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
