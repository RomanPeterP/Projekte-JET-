using FeedbackAppAP08.Data;
using FeedbackAppAP08.Logic;
using FeedbackAppAP08.Models;
using FeedbackAppAP08.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
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
        Feedback? feedback = HttpContext.Session.GetObject<Feedback>(KeyName);
        ViewData["Title"] = "Eingabe";
        return View(feedback);
    }

    [HttpPost]
    public async Task<IActionResult> Submit(Feedback feedback, [FromQuery] bool isDraw)
    {
        ViewBagCountries();
        if (!ModelState.IsValid)
            return View("Index", feedback);

        if (isDraw)
        {
            HttpContext.Session.SetObject(KeyName, feedback);
            return RedirectToAction("Index", feedback);
        }

        await AddDocs(feedback);

        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();
        HttpContext.Session.Remove(KeyName);
        TempData["Name"] = feedback.Name;

        return RedirectToAction("ThankYou");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Details(int id)
    {
        var viewmodel = new FeedbackDetailsViewModel();
        var feedback = _context.Feedbacks.Include(f => f.Documents).FirstOrDefault(f => f.FeedbackId == id);
        if (feedback == null)
            return RedirectToAction("List");
        viewmodel.Feedback = feedback;
        GenerateDocHtml(viewmodel, feedback);
        return View(viewmodel);
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

    private void GenerateDocHtml(FeedbackDetailsViewModel viewmodel, Feedback feedback)
    {
        if (feedback.Documents.Count == 0)
            return;

        var tagsList = new List<string>();
        foreach (var document in feedback.Documents)
        {
            TagBuilder tagBuilder;
            switch (document.Extension)
            {
                case FileExtensions.Jpg:
                case FileExtensions.Png:
                case FileExtensions.Jpeg:
                case FileExtensions.Bmp:
                case FileExtensions.Gif:
                case FileExtensions.Webp:
                    tagBuilder = new TagBuilder("img");
                    tagBuilder.Attributes.Add("src", Url.Action("Image", "Documents",
                        new { id = document.DocsId, isThumbnail = true }));
                    tagBuilder.Attributes.Add("onclick", "window.open('" + Url.Action("Image", "Documents",
                        new { id = document.DocsId, isThumbnail = false }) + "', 'bildgross', 'popup')");
                    WriteHtmlInList(tagsList, tagBuilder);
                    break;

                case FileExtensions.Mp4:
                    tagBuilder = new TagBuilder("video");
                    tagBuilder.Attributes.Add("width", "640");
                    tagBuilder.Attributes.Add("height", "360");
                    tagBuilder.Attributes.Add("controls", "controls");
                    var source = new TagBuilder("source");
                    source.Attributes.Add("src", Url.Action("Mp4", "Documents", new { id = document.DocsId }));
                    source.Attributes.Add("type", document.MimeType);
                    tagBuilder.InnerHtml.AppendHtml(source);
                    WriteHtmlInList(tagsList, tagBuilder);
                    break;

                default:
                    tagBuilder = new TagBuilder("a");
                    tagBuilder.Attributes.Add("href", Url.Action("Doc", "Documents",
                        new { id = document.DocsId }));
                    tagBuilder.Attributes.Add("target", "_blank");
                    tagBuilder.InnerHtml.Append("Dokument");
                    WriteHtmlInList(tagsList, tagBuilder);
                    break;
            }
        }
        viewmodel.DocsTags = tagsList;
    }

    private static void WriteHtmlInList(List<string> tagsList, TagBuilder tagBuilder)
    {
        using (var writer = new StringWriter())
        {
            tagBuilder.WriteTo(writer, HtmlEncoder.Default);
            tagsList.Add(writer.ToString());
        };
    }
    private static async Task AddDocs(Feedback feedback)
    {
        if (feedback.DocsFromWeb == null)
            return;

        foreach (var file in feedback.DocsFromWeb)
        {
            if (file == null || file.Length <= 0)
                continue;

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var doc = new Doc
            {
                Document = ms.ToArray(),
                Extension = Path.GetExtension(file.FileName).ToLower(),
                MimeType = file.ContentType
            };
            feedback.Documents.Add(doc);
        }
    }
}