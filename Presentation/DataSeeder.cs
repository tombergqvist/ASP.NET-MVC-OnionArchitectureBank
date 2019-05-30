using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Persistence;
using System.Collections.Generic;

namespace Presentation
{
    public class DataSeeder
    {
        public void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<string> { "admin", "cashier", "customer" };

            foreach (var roleName in roles)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    var role = new IdentityRole(roleName);
                    roleManager.CreateAsync(role).Wait();
                }
            }
        }

        public void SeedAdmin(UserManager<ApplicationUser> userManager, string password)
        {
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@admin.com"
                };
                userManager.CreateAsync(user, password).Wait();
                userManager.AddToRoleAsync(user, "admin").Wait();
            }
        }

        public void SeedCustomer(UserManager<ApplicationUser> userManager, int customerId, string name, string password)
        {
            if (userManager.FindByNameAsync(name).Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = name,
                    CustomerId = customerId
                };
                userManager.CreateAsync(user, password).Wait();
                userManager.AddToRoleAsync(user, "customer").Wait();
            }
        }
    }
}