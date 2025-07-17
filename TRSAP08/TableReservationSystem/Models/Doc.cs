namespace TableReservationSystem.Models;

public class Doc
{
    public int DocsId { get; set; }

    public int RestaurantId { get; set; }

    public byte[] Document { get; set; } = null!;

    public string? Extension { get; set; }

    public string MimeType { get; set; } = null!;

    public virtual Restaurant Restaurant { get; set; } = null!;
}
