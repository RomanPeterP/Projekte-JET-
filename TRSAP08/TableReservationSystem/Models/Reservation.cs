namespace TableReservationSystem.Models;

public class Reservation
{
    public int ReservationId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int ReservationTimeId { get; set; }

    public byte NumberOfGuests { get; set; }

    public int ContactInfoId { get; set; }

    public int RestaurantId { get; set; }

    public string? TableNumber { get; set; }

    public string? AdditionalMessage { get; set; }

    public byte ReservationStatusId { get; set; }
    public virtual ContactInfo ContactInfo { get; set; } = null!;
    public virtual ReservationStatus ReservationStatus { get; set; } = null!;
    public virtual ReservationTime ReservationTimes { get; set; } = null!;
    public virtual Restaurant Restaurant { get; set; } = null!;
    public virtual Table Table { get; set; } = null!;
}
