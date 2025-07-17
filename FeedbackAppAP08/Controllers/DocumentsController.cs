using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class DocumentsController : Controller
{
    private readonly FeedbackDbContext _context;
    public DocumentsController(FeedbackDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Image(int id, bool isThumbnail)
    {
        var doc = await _context.Docs.FindAsync(id);
        if (doc?.Document == null)
            return NotFound();
        var breite = isThumbnail ? 100 : 1000;
        var resizedImage = ResizeImage(doc.Document, breite);

        return File(resizedImage, doc.MimeType);
    }

    public async Task<IActionResult> Doc(int id)
    {
        var doc = await _context.Docs.FindAsync(id);
        if (doc?.Document == null)
            return NotFound();

        return File(doc.Document, doc.MimeType, "Dokument" + doc.Extension);
    }

    public async Task<IActionResult> Mp4(int id)
    {
        var doc = await _context.Docs.FindAsync(id);
        if (doc?.Document == null)
            return NotFound();
        return File(doc.Document, doc.MimeType);
    }

    public static byte[] ResizeImage(byte[] originalBild, int zielBreite)
    {
        using var inputStream = new MemoryStream(originalBild);
#pragma warning disable CA1416
        using var originalImage = System.Drawing.Image.FromStream(inputStream);

        var originalBreite = originalImage.Width;
        var originalHoehe = originalImage.Height;

        var faktor = (float)zielBreite / originalBreite;
        var zielHoehe = (int)(originalHoehe * faktor);

        using var resizedBitmap = new Bitmap(zielBreite, zielHoehe);

        using var graphics = Graphics.FromImage(resizedBitmap);

        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

        graphics.DrawImage(originalImage, 0, 0, zielBreite, zielHoehe);

        using var outputStream = new MemoryStream();
        resizedBitmap.Save(outputStream, ImageFormat.Jpeg);
#pragma warning restore CA1416
        return outputStream.ToArray();
    }
}