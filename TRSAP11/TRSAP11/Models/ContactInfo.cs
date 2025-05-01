using TRSAP11.Logic;

namespace TRSAP11.Models
{
    public class ContactInfo
    {
        public int Id { get; } = Tools.GetNextUniqueInteger;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public List<Reservation>? Reservations { get; set; }
        public List<Restaurant>? Restaurants { get; set; }
    }
}
