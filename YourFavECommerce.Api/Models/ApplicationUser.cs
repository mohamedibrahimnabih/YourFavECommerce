﻿using Microsoft.AspNetCore.Identity;

namespace YourFavECommerce.Api.Models
{
    public enum ApplicationUserGender
    {
        Male,
        Female
    }

    public class ApplicationUser : IdentityUser
    {
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ApplicationUserGender Gender { get; set; }
        public DateOnly BirthOfDate { get; set; }
    }
}
