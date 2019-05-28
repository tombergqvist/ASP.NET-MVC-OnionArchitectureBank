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
            return PartialView("_UserPartial", new UserViewModel()
            {
                User = await _userManager.FindByIdAsync(id)
            });
        }

        [HttpGet]
        public IActionResult NewUser()
        {
            return PartialView("_UserPartial", new UserViewModel()
            {
                User = new ApplicationUser()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUser(UserViewModel model)
        {
            model.Message = null;
            if (ModelState.IsValid)
            {
                var foundUser = await _userManager.FindByNameAsync(model.User.UserName);
                if (foundUser == null)
                {
                    await _userManager.CreateAsync(model.User, model.Password);
                    model.Message = "The user was created successfully. ";
                }
                else
                {
                    model.Message = $"A user named {model.User.UserName} already exists. ";
                }
            }
            return PartialView("_UserPartial", model);
        }
    }
}