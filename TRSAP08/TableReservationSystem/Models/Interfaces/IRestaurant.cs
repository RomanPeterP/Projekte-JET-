namespace TableReservationSystem.Models.Interfaces
{
    public interface IRestaurant
    {
        bool Activ { get; set; }
        string City { get; set; }
        ContactInfo ContactInfo { get; set; }
        int ContactInfoId { get; set; }
        string CountryCode { get; set; }
        Country Country { get; set; }
        string Name { get; set; }
        string PostalCode { get; set; }
        ICollection<Reservation> Reservations { get; set; }
        int RestaurantId { get; set; }
        string StreetHouseNr { get; set; }
        ICollection<Table> Tables { get; set; }
    }
}