using TRSAP11.Logic;

namespace TRSAP11.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; } = Tools.GetNextUniqueInteger;
        public string Name { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string StreetHouseNr { get; set; } = null!;
        public bool Activ { get; set; }
        public string Country { get; set; } = null!;
        public ContactInfo ContactInfo { get; set; } = null!;
        public List<Table>? Tables { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}
