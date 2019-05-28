using Persistence;

namespace Presentation.Models.Admin
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Message { get; set; }
    }
}
