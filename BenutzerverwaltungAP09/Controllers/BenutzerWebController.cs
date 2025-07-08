using BenutzerverwaltungAP09.Data;
using BenutzerverwaltungAP09.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            using BenutzerContext context = new BenutzerContext();
            
            // Anlegen
            if (!benutzer.BenutzerId.HasValue)
            {
                benutzer.LoginData.CreatedAt = DateTime.Now;
                context.Benutzer.Add(benutzer);
                context.SaveChanges();
                return RedirectToAction("ThankYou"); // Todo Frage warum keine Modelübergabe möglich?
            }
            else // Aktualisieren
            {
                benutzer.LoginData.ModifiedAt = DateTime.Now;
                context.Benutzer.Attach(benutzer);
                context.Entry(benutzer).State = EntityState.Modified;
                context.Entry(benutzer.LoginData).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("List");
            }
        }


        public IActionResult ThankYou()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            using BenutzerContext context = new BenutzerContext();
            IEnumerable<Benutzer> bl = context.Benutzer.AsNoTracking()
                .OrderBy(o => o.BenutzerId)
                .Include(o => o.LoginData)
                .ToList();
            return View(bl);
        }

        [HttpGet]
        public IActionResult Bearbeiten(int id)
        {
            using BenutzerContext context = new BenutzerContext();
            var benutzer = context.Benutzer.AsNoTracking().Include(b => b.LoginData)
                .FirstOrDefault(b => b.BenutzerId == id);

            return View("Registrieren", benutzer);
        }

        [HttpGet]
        public IActionResult Loeschen(int id)
        {
            using BenutzerContext context = new BenutzerContext();
            var benutzer = context.Benutzer.FirstOrDefault(b => b.BenutzerId == id);
            var logindata = context.LoginData.FirstOrDefault(b => b.BenutzerId == id);

            if (benutzer != null)
            {
                if (logindata != null)
                    context.LoginData.Remove(logindata);

                context.Benutzer.Remove(benutzer);
                context.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
