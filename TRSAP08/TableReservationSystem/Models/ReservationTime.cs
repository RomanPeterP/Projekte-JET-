namespace TableReservationSystem.Models;

public class ReservationTime
{
    public int ReservationTimeId { get; set; }

    public TimeOnly Time { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
