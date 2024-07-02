using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MVC.Models
{
    public class ChangeEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
