using System.ComponentModel.DataAnnotations;

namespace OnlineStore.WebAPI.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
