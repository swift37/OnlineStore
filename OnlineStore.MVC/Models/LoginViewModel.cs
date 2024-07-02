using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MVC.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? RedirectUrl { get; set; }
    }
}
