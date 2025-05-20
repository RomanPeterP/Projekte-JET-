using System.ComponentModel.DataAnnotations;
using TRSAP09.Logic;

namespace TRSAP09.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        [MaxLength(75)]
        public string Name { get; set; } = null!;
        [MaxLength(12)]
        public string PostalCode { get; set; } = null!;
        [MaxLength(30)]
        public string City { get; set; } = null!;
        [MaxLength(40)]
        public string StreetHouseNr { get; set; } = null!;
        public bool Activ { get; set; }
        [MaxLength(2)]
        public string Country { get; set; } = null!;
        public int ContactInfoId { get; set; }

        public ContactInfo ContactInfo { get; set; } = null!;

        public List<Table>? Tables { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}
