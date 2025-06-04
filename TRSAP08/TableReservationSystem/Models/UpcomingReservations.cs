namespace TableReservationSystem.Models
{
    public class UpcomingReservation
    {
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly Time { get; set; }
        public string RestaurantName { get; set; } = null!;
        public string TableNumber { get; set; } = null!;
        public string ContactName { get; set; } = null!;
        public string ReservationStatus { get; set; } = null!;
    }
}
