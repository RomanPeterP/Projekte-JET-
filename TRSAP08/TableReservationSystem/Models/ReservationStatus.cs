namespace TableReservationSystem.Models;

public class ReservationStatus
{
    public byte ReservationStatusId { get; set; }

    public string Caption { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
