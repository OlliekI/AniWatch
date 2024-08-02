using AniWatch.Data.Enums;
using AniWatch.Data.Static;
using AniWatch.Data.ViewModels;
using AniWatch.Models;
using AniWatch.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;
namespace AniWatch.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }




        [AllowAnonymous]
        public IActionResult Login() => View(new Login());

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                //login
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Animes");
                }
                ModelState.AddModelError("", "Invalid login attempt");
                return View();
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register() => View(new Register());

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Name = model.Name,
                    Email = model.Email,
                    UserName = model.Email,
                    Address = model.Address
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Animes");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        
        public async Task<IActionResult> Logout() 
        {
            await _signInManager?.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}
