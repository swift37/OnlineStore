using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Application.Models.Identity
{
    public class UpdateUserRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
