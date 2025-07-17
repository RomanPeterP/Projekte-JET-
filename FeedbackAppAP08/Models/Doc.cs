namespace FeedbackAppAP08.Models;

public partial class Doc
{
    public int DocsId { get; set; }

    public int FeedbackId { get; set; }

    public byte[] Document { get; set; } = null!;

    public string? Extension { get; set; }

    public string MimeType { get; set; } = null!;

    public virtual Feedback Feedback { get; set; } = null!;
}
