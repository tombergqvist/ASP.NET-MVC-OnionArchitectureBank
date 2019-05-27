using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a username")]
        [StringLength(50, ErrorMessage = "Username is too long, max 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
