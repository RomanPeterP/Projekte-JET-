namespace TRSAP09V2.Models;

public class Table
{
    public int TableId { get; set; }

    public string TableNumber { get; set; } = null!;

    public int RestaurantId { get; set; }

    public byte NumberOfSeats { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
