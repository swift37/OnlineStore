﻿using Microsoft.AspNetCore.Identity;

namespace OnlineStore.Identity.Models
{
    public class ApplicationUser : IdentityUser 
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }
    }
}
