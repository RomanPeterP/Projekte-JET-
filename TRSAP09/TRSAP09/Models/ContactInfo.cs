using System.ComponentModel.DataAnnotations;
using TRSAP09.Logic;

namespace TRSAP09.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        [MaxLength(128)]
        public string Email { get; set; } = null!;
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = null!;

        public List<Reservation>? Reservations { get; set; }
        public List<Restaurant>? Restaurants { get; set; }
    }
}
