using System.Collections;
using TRSAP11.Data;
using TRSAP11.Logic;
using TRSAP11.Models;

namespace TRSAP11
{
    public static class Uebungen
    {
        public static void AddRestaurant()
        {
            var restaurantLogic = new RestaurantLogic();

            // Ein Restaurant im System registrieren
            var restaurant = new Restaurant()
            {
                //Name = "Musil", // Auf doppelt prüfen
                Name = "Musil" + Guid.NewGuid().ToString(),
                ContactInfo = new ContactInfo
                {
                    Email = new EMail("musil@musil.at"),
                    PhoneNumber = new PhoneNumber("676767676"),
                },
                PostalCode = "1140",
                City = "Wien",
                StreetHouseNr = "Braillestr. 12",
                Country = Country.AT
            };

            var response = restaurantLogic.Register(restaurant);

            if (response.StatusCode == Enums.StatusCode.Success)
                Console.WriteLine(restaurant.Name + " wurde erfolgreich angelegt.");
            else
                Console.WriteLine("Fehler: " + response.Message);

            Console.WriteLine("---------------------------------------------------");

            // Daten von allen registrierten Restaurants abrufen
            response = restaurantLogic.Data();
            if (response.StatusCode == Enums.StatusCode.Success)
            {
                var data = response.Data;
                if (data != null)
                    foreach (var item in data.OrderBy(r => r.RestaurantId))
                        Console.WriteLine($"{item.RestaurantId} {item.Name}");
            }

            else
                Console.WriteLine("Fehler: " + response.Message);
        }

        public static void DeleteRestaurant()
        {
            var restaurantLogic = new RestaurantLogic();
            var restuarant = RestaurantData.RestaurantsList.FirstOrDefault();
            if (restuarant != null)
                restaurantLogic.Delete(restuarant.RestaurantId);
        }

        public static void UpdateRestaurant()
        {
            var restuarant = RestaurantData.RestaurantsList.FirstOrDefault();
            if (restuarant == null)
                return;
            restuarant.Name = "Neuer Name";
            var restaurantLogic = new RestaurantLogic();
            restaurantLogic.Update(restuarant);
        }


        public static void Polymorphie()
        {

            List<Reservation> reservations = new List<Reservation>
            {
                new VipReservation { Surcharge = 123.99m,  NumberOfGuests = 3, Date = new DateOnly(2025, 6, 1), Time = new TimeOnly(18, 9)},
                new FamilyReservation { IsHighChairRequired = true, NumberOfGuests = 3, Date = new DateOnly(2025, 6, 1), Time = new TimeOnly(18, 9) },
                new DefaultReservation { NumberOfGuests = 7, Date = new DateOnly(2025, 8, 10), Time = new TimeOnly(20, 4) }
            };

            foreach (Reservation reservation in reservations)
                Console.WriteLine(reservation.GetTypeDescription());

        }

        public static void Generics()
        {
            var tableList = new ManagementList<Table>();
            tableList.Add(new Table { TableNumber = "A34", NumberOfSeats = 4 });
            tableList.Add(new Table { TableNumber = "S12", NumberOfSeats = 2 });
            tableList.Display();

            var guestList = new ManagementList<Reservation>();

            guestList.Add(new DefaultReservation { Name = "Nikolaus", Date = new DateOnly(2025, 12, 12), Time = new TimeOnly(19, 15) });
            guestList.Add(new VipReservation { Name = "Erwin", Date = new DateOnly(2025, 6, 9), Time = new TimeOnly(17, 30) });

            guestList.Display();

        }
    }
}
