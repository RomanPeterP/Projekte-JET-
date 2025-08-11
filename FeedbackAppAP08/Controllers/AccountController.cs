using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FeedbackAppAP08.Controllers
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


            // Prüft, ob der eingegebene Benutzername (ohne führende/trailing Leerzeichen und unabhängig von Groß-/Kleinschreibung)
            // mit dem gespeicherten Benutzernamen übereinstimmt UND ob das Passwort exakt mit dem gespeicherten übereinstimmt.
            if (username.Trim().ToLower() == storedUsername.Trim().ToLower() && password == storedPassword)
            {
                // Erstellen einer Liste von Claims (Ansprüchen),
                // die dem Benutzer zugeordnet werden sollen.
                // Hier: Benutzername und Rolle ("Admin").
                var claims = new List<Claim>
                {
                    // Claim für den Benutzernamen (zur späteren Identifikation).
                    new Claim(ClaimTypes.Name, username),
                      // Claim für die Rolle; ermöglicht rollenbasierte Autorisierung.
                    new Claim(ClaimTypes.Role, "Admin")
                };

                // Erstellen einer Identität (Identity) auf Basis der Claims
                // mit dem Authentifizierungsschema "FeedbackCookieAuth".
                var identity = new ClaimsIdentity(claims, "FeedbackCookieAuth");

                // Erstellen eines ClaimsPrincipal – das Sicherheitsobjekt,
                // das die Identität(en) eines Benutzers enthält.
                var principal = new ClaimsPrincipal(identity);

                // Benutzer mit dem erstellten Principal am aktuellen HTTP-Kontext anmelden.
                // Dies erzeugt ein Authentifizierungs-Cookie basierend auf dem angegebenen Schema.
                await HttpContext.SignInAsync("FeedbackCookieAuth", principal);

                // Nach erfolgreicher Anmeldung: Weiterleitung zur Liste-Seite
                // des "Feedback"-Controllers.
                return RedirectToAction("List", "Feedback");
            }


            ViewBag.Error = "Login fehlgeschlagen!";
            return View();
        }
        
        [HttpGet] // Neu sdsd
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("FeedbackCookieAuth");
            return RedirectToAction("Index", "Feedback");
        }

        public IActionResult AccessDenied()
        {
            return Content("Zugriff verweigert.");
        }

    }

}
