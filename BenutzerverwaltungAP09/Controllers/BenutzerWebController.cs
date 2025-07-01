using BenutzerverwaltungAP09.Data;
using BenutzerverwaltungAP09.Models;
using Microsoft.AspNetCore.Mvc;

namespace BenutzerverwaltungAP09.Controllers
{
    public class BenutzerWebController : Controller
    {
        public IActionResult Registrieren()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrieren(Benutzer benutzer)
        {
            if (!ModelState.IsValid)
                return View(benutzer);

            benutzer.LoginData.CreatedAt = DateTime.Now;

            using BenutzerContext context = new BenutzerContext();
            context.Benutzer.Add(benutzer);
            context.SaveChanges();

            return RedirectToAction("ThankYou"); // Todo Frage warum keine Modelübergabe möglich?
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
           // TODO
            return View();
        }
    }
}
