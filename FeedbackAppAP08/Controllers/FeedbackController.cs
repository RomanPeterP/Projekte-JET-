using FeedbackAppAP08.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class FeedbackController : Controller
{
    private readonly FeedbackDbContext _context;
    private const string KeyName = "Feedback";

    public FeedbackController(FeedbackDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBagCountries();

        Feedback? feedback = null;

        var feedbackJson = HttpContext.Session.GetString(KeyName);
        if (feedbackJson != null)
        {
            feedback = JsonSerializer.Deserialize<Feedback>(feedbackJson);
        }


        ViewData["Title"] = "Eingabe";
        return View(feedback);
    }

    [HttpPost]
    public async Task<IActionResult> Submit(Feedback feedback, [FromQuery] bool isDraw)
    {
        ViewBagCountries();
        if (!ModelState.IsValid)
            return View("Index", feedback);

        if (!isDraw)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove(KeyName);
            TempData["Name"] = feedback.Name;

            return RedirectToAction("ThankYou");
        }

        if (isDraw)
        {
            string json = JsonSerializer.Serialize(feedback);
            HttpContext.Session.SetString(KeyName, json);

            return RedirectToAction("Index", feedback);
        }

        return View();
    }

    public IActionResult ThankYou()
    {
        ViewData["Title"] = "Danke";

        var bgColor = HttpContext.Request.Cookies["bgColor"];
        if (bgColor != "gold")
            HttpContext.Response.Cookies.Append("bgColor", "gold");

        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult List(FeedbackListViewModel? inViewmodel)
    {
        // mappen von hereinkommendem Viewmodel in das hinausgehende,
        // um Suchwörter im Inputfeld wiederherstellen / anzeigen zu können
        var outViewmodel = new FeedbackListViewModel
        {
            FeedbackSearchCriteria = inViewmodel?.FeedbackSearchCriteria
        };

        var begriffe = new List<string>();
        var words = inViewmodel?.FeedbackSearchCriteria?.Words;
        if (!string.IsNullOrWhiteSpace(words))
        {
            var arr = words.Split(" ");
            foreach (var word in arr)
            {
                if (!string.IsNullOrWhiteSpace(word))
                    begriffe.Add(word.Trim());
            }
        }

        // Einfache Abfrage aller Datensätze- falls keine Scuhbegriffe übergeben,
        if (begriffe.Count == 0)
            outViewmodel.FeedbackList = _context.Feedbacks.OrderByDescending(f => f.SubmittedAt);
        else
        {
            // ... sonst Filtern nach Suchwörtern
            outViewmodel.FeedbackList = _context.Feedbacks
            .Where(r =>
                begriffe.ToArray().All(w =>
                    r.Name.Contains(w) ||
                    r.Country.Contains(w) ||
                    r.Message.Contains(w) ||
                    r.Ratinggrade.ToString().Contains(w)
                )).OrderByDescending(f => f.SubmittedAt);

        }
        return View(outViewmodel);
    }

    private void ViewBagCountries()
    {
        ViewBag.Countries = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "" },
            new SelectListItem { Value = "AT", Text = "Österreich" },
            new SelectListItem { Value = "DE", Text = "Deutschland" },
            new SelectListItem { Value = "CH", Text = "Schweiz" },
        };
    }
}