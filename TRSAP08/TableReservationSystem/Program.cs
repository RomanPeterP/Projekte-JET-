using TableReservationSystem.Data;
using TableReservationSystem.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TableReservationSystem.Logic;
using Microsoft.Extensions.Configuration;
using TableReservationSystem.Models.Interfaces;
using Microsoft.Extensions.Options;

namespace TableReservationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Dependency Injection setzen
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<TableReservationSystemContext>(options =>
                        options.UseSqlServer(Config.ConfigItems.GetConnectionString("Default")));

                    services.AddScoped<TableReservationSystemContext, TableReservationSystemContext>();
                    services.AddScoped<IRestaurant, Restaurant>();
                    services.AddScoped<IRestaurantLogic, RestaurantLogic>();
                    services.AddScoped<IMiscLogic, MiscLogic>();
                    services.AddScoped<IResponse<IRestaurant>, Response<IRestaurant>>();
                    services.AddScoped<IResponse<Country>, Response<Country>>();
                    services.AddScoped<IRestaurantRepository, RestaurantRepository>();
                    services.AddScoped<IMiscRepository, MiscRepository>();
                })
                .Build();


            // Dependency Injection  verwenden
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var logic = serviceProvider.GetRequiredService<IRestaurantLogic>();

            AddRestaurant(logic, true);
            //ListAllRestaurants(logic);

        }

        private static void AddRestaurant(IRestaurantLogic logic, bool withDelete)
        {
            var restaurant = new Restaurant()
            {
                //Name = "Musil",
                Name = ("Musil" + Guid.NewGuid().ToString()).Substring(0, 30), // zum Testen: Eindeutigkeit erzwingen
                ContactInfo = new ContactInfo
                {
                    Email = "musilX@musil.at",
                    PhoneNumber = "1234567890"
                },
                PostalCode = "1140",
                City = "Wien",
                StreetHouseNr = "Braillestr. 12",
                CountryCode = "AT",
                Activ = true
            };


            var tableNummer = Guid.NewGuid().ToString().Substring(0, 10);
            restaurant.Tables.Add(new Table() { Activ = true, TableNumber = tableNummer, NumberOfSeats = 4 });

            var reservation = new Reservation()
            {
                TableNumber = tableNummer,
                ReservationTimeId = 1,
                NumberOfGuests = 4,
                Name = "Roman",
                Date = new DateOnly(2025, 12, 12),
                ContactInfo = new ContactInfo() { Email = "romanpocX@at.at", PhoneNumber = "3245324234" }
            };
            restaurant.Reservations.Add(reservation);
            var reservation2 = new Reservation()
            {
                TableNumber = tableNummer,
                ReservationTimeId = 1,
                NumberOfGuests = 4,
                Name = "Anna",
                Date = new DateOnly(2025, 12, 20),
                ContactInfo = new ContactInfo() { Email = "romandpocX@at.at", PhoneNumber = "3245324234" }
            };
            restaurant.Reservations.Add(reservation2);


            var response = logic.Register(restaurant);

            if (response.StatusCode == Enums.StatusCode.Success)
                Console.WriteLine(restaurant.Name + " wurde erfolgreich angelegt.");
            else
                Console.WriteLine("Fehler: " + response.Message);

            Console.WriteLine("---------------------------------------------------");

            // DELETE
            if (withDelete)
                response = logic.Delete(restaurant);
        }

        private static void ListAllRestaurants(IRestaurantLogic logic)
        {
            // Daten von allen registrierten Restaurants abrufen
            var response = logic.Data(null);
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
    }
}
