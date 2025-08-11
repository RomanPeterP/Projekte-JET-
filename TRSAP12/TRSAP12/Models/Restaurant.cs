namespace TRSAP12.Models
{
    internal class Restaurant
    {
        public string Name { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string StreetHouseNr { get; set; } = null!;
        public bool Activ { get; set; }
        public Country Country { get; set; }
        public ContactInfo ContactInfo { get; set; } = null!;
        public List<Table> Tables { get; set; } = null!;
    }
}
