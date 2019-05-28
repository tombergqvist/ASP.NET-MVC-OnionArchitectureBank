using Microsoft.AspNetCore.Identity;
using Persistence;
using System.Collections.Generic;

namespace Presentation.Models.Admin
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Message { get; set; }
        public string Role { get; set; }
        public List<IdentityRole> AvailableRoles { get; set; }
    }
}
