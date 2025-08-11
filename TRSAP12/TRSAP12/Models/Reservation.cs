namespace TRSAP12.Models
{
    internal class Reservation
    {
        public string Name { get; set; } = null!;
        public uint NumberOfGuests { get; set; }
        public string? AdditionalMessage { get; set; }
        public TimeOnly Time { get; set; }
        public DateOnly Date { get; set; }
        public ReservationStatus Status { get; set; }
        public Restaurant Restaurant { get; set; } = null!;
        public ContactInfo ContactInfo { get; set; } = null!;
        public Table? Table { get; set; }
    }
}
