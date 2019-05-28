using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Presentation.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AdminController: Controller
    {
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult ManageUsers()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            return PartialView("_UserPartial", new UserViewModel()
            {
                User = user,
                AvailableRoles = _roleManager.Roles.ToList(),
                Role = roles.Count == 0 ? null : roles[0]
            });
        }

        [HttpGet]
        public IActionResult NewUser()
        {
            return PartialView("_UserPartial", new UserViewModel()
            {
                User = new ApplicationUser(),
                AvailableRoles = _roleManager.Roles.ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUser(UserViewModel model)
        {
            model.Message = null;
            if (ModelState.IsValid)
            {
                var user = model.User;
                var foundUser = await _userManager.FindByNameAsync(user.UserName);
                if (foundUser == null )
                {
                    await _userManager.CreateAsync(user, model.Password);

                    user = await _userManager.FindByNameAsync(user.UserName);
                    model.Message = $"The user named {user.UserName} was created successfully. ";
                    await SetRole(user, model.Role);
                }
                else if (foundUser.Id == user.Id)
                {
                    foundUser.UserName = user.UserName;
                    foundUser.PhoneNumber = user.PhoneNumber;
                    foundUser.Email = user.Email;
                    if (model.Password != null)
                    {
                        foundUser.PasswordHash = _userManager.PasswordHasher.HashPassword(foundUser, model.Password);
                    }
                    model.Message = $"The user was updated successfully. ";
                    await SetRole(foundUser, model.Role);
                }
                else
                {
                    model.Message = $"A user named {model.User.UserName} already exists. ";
                }
            }
            model.AvailableRoles = _roleManager.Roles.ToList();
            return PartialView("_UserPartial", model);
        }

        private async Task SetRole(ApplicationUser user, string role)
        {
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, role);
        }
    }
}