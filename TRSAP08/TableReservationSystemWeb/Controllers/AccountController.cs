using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TableReservationSystemWeb.Controllers
{

    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var adminSection = _config.GetSection("AdminCredentials");
            string storedUsername = adminSection["Username"]!;
            string storedPassword = adminSection["Password"]!;


            if (username.Trim().ToLower() == storedUsername.Trim().ToLower()
                && password == storedPassword)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };

                var identity = new ClaimsIdentity(claims, "TRSCookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("TRSCookieAuth", principal);

                return RedirectToAction("List", "Restaurant");
            }

            ViewBag.Error = "Login fehlgeschlagen!";
            return View();
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
