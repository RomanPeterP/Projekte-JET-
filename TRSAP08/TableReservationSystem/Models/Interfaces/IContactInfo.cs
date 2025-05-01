namespace TableReservationSystem.Models.Interfaces
{
    public interface IContactInfo
    {
        int ContactInfoId { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        ICollection<Reservation> Reservations { get; set; }
        ICollection<Restaurant> Restaurants { get; set; }
    }
}