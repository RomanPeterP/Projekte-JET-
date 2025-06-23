namespace TRSAP09V2.Models;

public class Restaurant
{
    public int RestaurantId { get; set; }

    public string Name { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string City { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public string StreetHouseNr { get; set; } = null!;

    public bool IsActive { get; set; }

    public int ContactInfoId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastUpdateAt { get; set; }

    public virtual ContactInfo ContactInfo { get; set; } = null!;

    public virtual Country Countries { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}
