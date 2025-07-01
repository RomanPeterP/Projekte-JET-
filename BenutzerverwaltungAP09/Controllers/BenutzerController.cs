using BenutzerverwaltungAP09.Data;
using BenutzerverwaltungAP09.Models;
using Microsoft.AspNetCore.Mvc;

namespace BenutzerverwaltungAP09.Controllers
{
    [ApiController]
    [Route("benutzer")]
    public class BenutzerController : ControllerBase
    {
        [HttpPost("Registrieren")]
        public IActionResult Registrieren([FromForm] Benutzer benutzer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using BenutzerContext context = new BenutzerContext();
            context.Benutzer.Add(benutzer);
            context.SaveChanges();
            return Ok($"Registrierung erfolgreich: {benutzer.Vorname} {benutzer.Nachname}, Alter: {benutzer.Alter}");
        }


        [HttpPost("RegistrierenJson")]
        public IActionResult RegistrierenJson([FromBody] Benutzer benutzer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using BenutzerContext context = new BenutzerContext();
            context.Benutzer.Add(benutzer);
            context.SaveChanges();
            return Ok(benutzer);
        }

        [HttpGet("{nachname}")]
        public IActionResult GetBenutzer([FromRoute] string nachname)
        {
            using BenutzerContext context = new BenutzerContext();
            var benutzer = context.Benutzer.FirstOrDefault(b => b.Nachname == nachname);
            return benutzer != null ? Ok(benutzer) : NotFound("Benutzer nicht gefunden");
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            using BenutzerContext context = new BenutzerContext();
            var benutzer = context.Benutzer.ToList();
            return benutzer != null ? Ok(benutzer) : NotFound("Benutzer nicht gefunden");
        }

    }
}
