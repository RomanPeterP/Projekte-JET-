using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using System;

[Authorize(Roles = "Admin")]
public class DocumentsController : Controller
{
    private readonly FeedbackDbContext _context;
    private readonly IMemoryCache _cache;
    public DocumentsController(FeedbackDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IActionResult> Image(int id, bool isThumbnail)
    {
        
        // TEST sdfcdsafdsaf
        var cacheKey = $"doc_{id}_{(isThumbnail ? "thumb" : "full")}";

        if (!_cache.TryGetValue(cacheKey, out (byte[] Data, string MimeType) cachedImage))
        {
            var doc = await _context.Docs.FindAsync(id);
            if (doc?.Document == null)
                return NotFound();

            var breite = isThumbnail ? 100 : 1000;
            var resizedImage = ResizeImage(doc.Document, breite);

            cachedImage = (resizedImage, doc.MimeType);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30)); 

            _cache.Set(cacheKey, cachedImage, cacheEntryOptions);
        }

        return File(cachedImage.Data, cachedImage.MimeType);
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