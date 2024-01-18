using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MVC.Models
{
    public class ResetPasswordRequestViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }
    }
}
