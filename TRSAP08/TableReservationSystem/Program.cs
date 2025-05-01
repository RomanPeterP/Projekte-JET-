using TableReservationSystem.Data;
using TableReservationSystem.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TableReservationSystem.Logic;
using Microsoft.Extensions.Configuration;
using TableReservationSystem.Models.Interfaces;

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
                        options.UseSqlServer(Config.ConfigItems.GetConnectionString("default")));
                    
                    services.AddScoped<TableReservationSystemContext, TableReservationSystemContext>();
                    services.AddScoped<IRestaurant, Restaurant>();
                    services.AddScoped<IRestaurantLogic, RestaurantLogic>();
                    services.AddScoped<IResponse, Response>();
                    services.AddScoped<IRestaurantRepository, RestaurantRepository>();
                })
                .Build();


            // Dependency Injection  verwenden
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var logic = serviceProvider.GetRequiredService<IRestaurantLogic>();


            var restaurant = new Restaurant()
            {
                //Name = "Musil",
                Name = ("Musil" + Guid.NewGuid().ToString()).Substring(0, 30), // zum Testen: Eindeutigkeit erzwingen
                ContactInfo = new ContactInfo
                {
                    Email = "musil@musil.at",
                    PhoneNumber = "1234567890"
                },
                PostalCode = "1140",
                City = "Wien",
                StreetHouseNr = "Braillestr. 12",
                CountryCode = "AT",
                Activ = true
            };


            var response = logic.Register(restaurant);

            if (response.StatusCode == Enums.StatusCode.Success)
                Console.WriteLine(restaurant.Name + " wurde erfolgreich angelegt.");
            else
                Console.WriteLine("Fehler: " + response.Message);

            Console.WriteLine("---------------------------------------------------");

            // Daten von allen registrierten Restaurants abrufen
            response = logic.Data();
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
