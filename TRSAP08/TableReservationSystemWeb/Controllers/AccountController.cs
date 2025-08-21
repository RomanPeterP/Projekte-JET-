using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TableReservationSystem.Data;
using TableReservationSystem.Models;
using TableReservationSystem.Viewmodels;

namespace TableReservationSystemWeb.Controllers
{

    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<User> _hasher;
        private readonly TableReservationSystemContext _context;

        public AccountController(IConfiguration config, IPasswordHasher<User> hasher, TableReservationSystemContext context)
        {
            _config = config;
            _hasher = hasher;
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users
                .FirstOrDefaultAsync(b => string.Equals(b.UserName, model.UserName)
                || string.Equals(b.Email, model.Email));

            if (user != null)
            {
                ModelState.AddModelError("", "Username or Email already in use.");
                return View(model);
            }

            var userRole = _context.Roles.FirstOrDefault(r => r.RoleName == "User");

            if (userRole == null)
            {
                userRole = new Role
                {
                    RoleName = "User"
                };
                await _context.Roles.AddAsync(userRole);
                await _context.SaveChangesAsync();
            }

            user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                Role = userRole
            };

            user.PasswordHash = _hasher.HashPassword(user, model.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(b => b.UserName == model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View(model);
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
            if (result != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("", "Wrong password.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            var identity = new ClaimsIdentity(claims, "TRSCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("TRSCookieAuth", principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("TRSCookieAuth");
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return Content("Zugriff verweigert.");
        }

    }

}
