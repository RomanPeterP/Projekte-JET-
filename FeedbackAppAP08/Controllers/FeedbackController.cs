using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FeedbackController : Controller
{
    private readonly FeedbackDbContext _context;

    public FeedbackController(FeedbackDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.Countries = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "" },
            new SelectListItem { Value = "AT", Text = "Österreich" },
            new SelectListItem { Value = "DE", Text = "Deutschland" },
            new SelectListItem { Value = "CH", Text = "Schweiz" },
        };

        ViewData["Title"] = "Eingabe";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Submit(Feedback feedback)
    {
        if (!ModelState.IsValid)
            return View("Index", feedback);

        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();

        TempData["Name"] = feedback.Name;

        return RedirectToAction("ThankYou");
    }

    public IActionResult ThankYou()
    {
        ViewData["Title"] = "Danke";

        var bgColor = HttpContext.Request.Cookies["bgColor"];
        if (bgColor != "gold")
            HttpContext.Response.Cookies.Append("bgColor", "gold");

        return View();
    }
}