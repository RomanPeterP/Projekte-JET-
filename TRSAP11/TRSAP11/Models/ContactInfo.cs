using TRSAP11.Logic;

namespace TRSAP11.Models
{
    public class ContactInfo
    {
        public int Id { get; } = Tools.GetNextUniqueInteger;
        public EMail Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public List<Reservation>? Reservations { get; set; }
        public List<Restaurant>? Restaurants { get; set; }
    }
}
