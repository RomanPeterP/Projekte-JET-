namespace TRSAP12.Models
{
    internal class ContactInfo
    {
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public List<Reservation>? Reservations { get; set; }
        public List<Restaurant>? Restaurants { get; set; }
    }
}
