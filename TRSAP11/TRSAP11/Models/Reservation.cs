using TRSAP11.Logic;

namespace TRSAP11.Models;

public abstract class Reservation
{
    public int Id { get; } = Tools.GetNextUniqueInteger;
    public string Name { get; set; } = null!;
    public byte NumberOfGuests { get; set; }
    public string? AdditionalMessage { get; set; }
    public TimeOnly Time { get; set; }
    public DateOnly Date { get; set; }
    public ContactInfo ContactInfo { get; set; } = null!;
    public ReservationStatus Status { get; set; } = ReservationStatus.Requested;
    public Restaurant Restaurant { get; set; } = null!;
    public Table Table { get; set; } = null!;
}
