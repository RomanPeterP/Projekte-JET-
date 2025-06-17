using BenutzerverwaltungAP08.Models;
using EFCNorthwindUebungen.Data;
using Microsoft.AspNetCore.Mvc;

namespace BenutzerverwaltungAP08.Controller
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
            return Ok(benutzer);
        }
    }
}
