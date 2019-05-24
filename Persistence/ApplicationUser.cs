using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class ApplicationUser: IdentityUser
    {
        public int? CustomerId { get; set; }
    }
}
