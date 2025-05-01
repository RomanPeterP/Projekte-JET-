namespace TableReservationSystem.Models;

public class Table
{
    public required string TableNumber { get; set; }

    public byte NumberOfSeats { get; set; }

    public int RestaurantId { get; set; }

    public bool Activ { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
