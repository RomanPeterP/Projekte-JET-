namespace TRSAP09V2.Models;

public class ReservationStatus
{
    public int ReservationStatusId { get; set; }

    public string Caption { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
