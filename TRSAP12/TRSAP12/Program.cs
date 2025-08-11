using TRSAP12.Models;

namespace TRSAP12
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var vr = new VipReservation()
            {
                ContactInfo = new ContactInfo(),
                Date = new DateOnly(2025, 8, 12),
                Time = new TimeOnly(12,0),
                HasWelcomeDrink = false,
                Name = "Roman",
            };

            var fr = new FamilyReservation()
            {
                ContactInfo = new ContactInfo(),
                Date = new DateOnly(2025, 8, 12),
                Time = new TimeOnly(12, 0),
                IsHighChairRequired = true,
                Name = "Tobias",
            };

            List<Reservation> reservations = new List<Reservation>();
            reservations.Add(vr);
            reservations.Add(fr);


            foreach (var reservation in reservations)
            {
                Console.WriteLine(reservation.GetInfo());
            }
 // Test
        }
    }
}
