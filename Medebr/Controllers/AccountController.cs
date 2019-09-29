using Medebr.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medebr.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signinManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager)
        {
            this.signinManager = signinManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        
        public IActionResult Login(string returnUrl)
        {

            return View(new User()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var getUser = await userManager.FindByNameAsync(user.UserName);
                if (getUser != null)
                {
                    var result = await signinManager.PasswordSignInAsync(getUser, user.Password, false, false);
                    if (result.Succeeded)
                    {
                        if (string.IsNullOrEmpty(user.ReturnUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        return RedirectToAction(user.ReturnUrl);
                    }
                }
                ModelState.AddModelError("", "Username/Password not found");
            }

            return View(user);
        }
        
        public IActionResult Register()
        {
            return View();
        }

        public async Task CreateRoles()
        {
            bool exist = await roleManager.RoleExistsAsync("ADMIN");
            if (!exist)
            {
                var role = new IdentityRole
                {
                    Name = "ADMIN"
                };
                await roleManager.CreateAsync(role);
            }

            exist = await roleManager.RoleExistsAsync("USER");
            if (!exist)
            {
                var role = new IdentityRole
                {
                    Name = "USER"
                };
                await roleManager.CreateAsync(role);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                await CreateRoles();
                var registerUser = new IdentityUser() { UserName = user.UserName, Email = user.Email };
                var result = await userManager.CreateAsync(registerUser, user.Password);
                await userManager.AddToRoleAsync(registerUser, "USER");
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
            }
            return View(user);

        }
        public async Task<IActionResult> Logout()
        {
            await signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
    }
}
