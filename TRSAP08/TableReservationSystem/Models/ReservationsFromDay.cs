namespace TableReservationSystem.Models
{
    public class ReservationsFromDay
    {
        public int ReservationId { get; set; }
        public TimeOnly Time { get; set; }
        public string RestaurantName { get; set; } = null!;
    }
}
