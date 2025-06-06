﻿using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TableReservationSystem.Data;
using TableReservationSystem.Models;


namespace TableReservationSystemTests
{
    public class Modul4TestVorbereitung
    {
        public static void Uebungen()
        {
            using var context = new TableReservationSystemContext();

            // 1. Alle Restaurants anzeigen
            Console.WriteLine("1. Alle Restaurants:");
            var allRestaurants = context.Restaurant.ToList();
            foreach (var r in allRestaurants)
                Console.WriteLine(r.Name);

            // 2. Alle Reservierungen mit Restaurantnamen anzeigen (Eager Loading)
            Console.WriteLine("2. Reservierungen mit Restaurantnamen:");
            var reservations = context.Reservation.Include(r => r.Restaurant).ToList();
            foreach (var res in reservations)
                Console.WriteLine($"- {res.Name} bei {res.Restaurant?.Name}");

            // 3. Neue Reservierung mit erstem Restaurant, Zeit und Kontakt aus DB
            Console.WriteLine("3. Neue Reservierung:");
            var firstRestaurant = context.Restaurant.FirstOrDefault();
            var firstTime = context.ReservationTime.FirstOrDefault();
            var firstContact = context.ContactInfo.FirstOrDefault();
            var firstStatus = context.ReservationStatus.FirstOrDefault();

            if (firstRestaurant != null && firstTime != null && firstContact != null && firstStatus != null)
            {
                var newRes = new Reservation
                {
                    Name = $"Gast {DateTime.Now:HHmmss}",
                    Date = new DateOnly(2025, 12, 12),
                    RestaurantId = firstRestaurant.RestaurantId,
                    ReservationTimeId = firstTime.ReservationTimeId,
                    ContactInfoId = firstContact.ContactInfoId,
                    ReservationStatusId = firstStatus.ReservationStatusId
                };
                context.Reservation.Add(newRes);
                context.SaveChanges();
                Console.WriteLine($"Neue Reservierung für {newRes.Name} gespeichert.");
            }

            // 4. Beliebige Reservierung ändern
            Console.WriteLine("4. Erste Reservierung ändern:");
            var resToChange = context.Reservation.FirstOrDefault();
            if (resToChange != null)
            {
                resToChange.Name = "geändert";
                context.SaveChanges();
                Console.WriteLine("Name geändert.");
            }

            // 5. Letzte Reservierung löschen
            Console.WriteLine("5. Letzte Reservierung löschen:");
            var lastRes = context.Reservation.OrderByDescending(r => r.ReservationId).FirstOrDefault();
            if (lastRes != null)
            {
                context.Reservation.Remove(lastRes);
                context.SaveChanges();
                Console.WriteLine($"Reservierung {lastRes.Name} gelöscht.");
            }

            // 6. Alle Länder auflisten
            Console.WriteLine("6. Länder:");
            foreach (var c in context.Country)
                Console.WriteLine($"- {c.Name}");

            // 7. Restaurant inkl. zugehörigem Land anzeigen (Eager Loading)
            Console.WriteLine("7. Restaurants mit Land:");
            var restaurantsWithCountry = context.Restaurant.Include(r => r.Country).ToList();
            foreach (var r in restaurantsWithCountry)
                Console.WriteLine($"- {r.Name} ({r.Country?.Name})");

            // 8. Restaurant hinzufügen
            Console.WriteLine("8. Restaurant hinzufügen:");
            context.Restaurant.Add(new Restaurant
            {
                Name = "Tasty Time",
                CountryCode = "DE",
                City = "München",
                PostalCode = "23323",
                StreetHouseNr = "Wienerstr. 11",
                ContactInfo = new ContactInfo { Email = "sdsd@sdsd.de", PhoneNumber = "34324234" }
            });
            context.SaveChanges();

            // 9. Restaurant ändern
            Console.WriteLine("9. Restaurant ändern:");
            var resto = context.Restaurant.FirstOrDefault(r => r.Name == "Tasty Time");
            if (resto != null)
            {
                resto.Name = "Tasty Time Deluxe";
                context.SaveChanges();
            }

            // 10. Reservierungen für ein bestimmtes Restaurant (z.B. ID 1)
            Console.WriteLine("10. Reservierungen für Restaurant 1:");
            var resForRestaurant = context.Reservation.Where(r => r.RestaurantId == 1).ToList();
            foreach (var r in resForRestaurant)
                Console.WriteLine($"- {r.Name}");

            // 11. ContactInfo mit zugehörigen Restaurants (Eager Loading)
            Console.WriteLine("11. ContactInfos mit Restaurants:");
            var contactInfos = context.ContactInfo.Include(c => c.Restaurants).ToList();
            foreach (var c in contactInfos)
            {
                Console.WriteLine($"{c.Email}:");
                foreach (var r in c.Restaurants)
                    Console.WriteLine($"  - {r.Name}");
            }

            // 12. Alle verfügbaren Zeiten anzeigen
            Console.WriteLine("12. Reservierungszeiten:");
            foreach (var t in context.ReservationTime)
                Console.WriteLine($"{t.ReservationTimeId}: {t.Time}");

            // 13. Alle Reservierungen inklusive Zeit anzeigen
            Console.WriteLine("13. Reservierungen mit Zeit:");
            var reservationsWithTime = context.Reservation.Include(r => r.ReservationTimes).ToList();
            foreach (var r in reservationsWithTime)
                Console.WriteLine($"{r.Name} um {r?.ReservationTimes?.Time}");


            // 14. ReservationStatus ändern
            Console.WriteLine("14. Status einer Reservierung ändern:");
            var resStatus = context.Reservation.FirstOrDefault();
            if (resStatus != null)
            {
                resStatus.ReservationStatusId = 2;
                context.SaveChanges();
            }

            // 15. Change Tracking demonstrieren
            Console.WriteLine("15. Change Tracking:");
            var tracked = context.Restaurant.FirstOrDefault();
            if (tracked != null)
            {
                tracked.Name = "Geänderter Name";
                var changes = context.ChangeTracker.Entries()
                    .Where(e => e.State != EntityState.Unchanged)
                    .ToList();
                Console.WriteLine($"Änderungen: {changes.Count}");
                context.SaveChanges();
            }

            // 16. Restaurant inkl. ContactInfo und Country (ThenInclude)
            Console.WriteLine("16. Restaurant mit Kontakt und Land:");
            var fullData = context.Restaurant
                .Include(r => r.ContactInfo)
                .Include(r => r.Country)
                .ToList();
            foreach (var r in fullData)
                Console.WriteLine($"{r.Name}: {r.ContactInfo?.Email}, {r.Country?.Name}");

            // 17. Reservierung inkl. Kontakt, Zeit, Restaurant, Status (Mehrfaches Include)
            Console.WriteLine("17. Reservierung inkl. aller Infos:");
            var detailedRes = context.Reservation
                .Include(r => r.ContactInfo)
                .Include(r => r.ReservationTimes)
                .Include(r => r.Restaurant)
                .Include(r => r.ReservationStatus)
                .ToList();
            foreach (var r in detailedRes)
                Console.WriteLine($"{r.Name}, {r.Date}, {r.ReservationTimes?.Time}, {r.Restaurant?.Name}, {r.ReservationStatus?.Caption}");

            // 18. Restaurant mit zugehörigen Reservierungen anzeigen (Eager Loading)
            Console.WriteLine("18. Restaurants mit Reservierungen:");
            var restoWithRes = context.Restaurant.Include(r => r.Reservations).ToList();
            foreach (var r in restoWithRes)
            {
                Console.WriteLine($"{r.Name}:");
                foreach (var res in r.Reservations)
                    Console.WriteLine($"  - {res.Name}");
            }

            // 19. Löschen Sie alle Reservierungen mit Datum in der Vergangenheit.
            Console.WriteLine("19. Alte Reservierungen löschen:");
            var alteReservierungen = context.Reservation
                .Where(r => r.Date < DateOnly.FromDateTime(DateTime.Today))
                .ToList();
            context.Reservation.RemoveRange(alteReservierungen);
            context.SaveChanges();
            Console.WriteLine($"{alteReservierungen.Count} alte Reservierung(en) gelöscht.");

            // 20. Neues Land „DE – Deutschland“ erstellen (falls noch nicht vorhanden)
            Console.WriteLine("20. Land hinzufügen, falls nicht vorhanden:");
            if (!context.Country.Any(c => c.CountryCode == "CH"))
            {
                context.Country.Add(new Country
                {
                    CountryCode = "CH",
                    Name = "Schweiz"
                });
                context.SaveChanges();
                Console.WriteLine("Land 'Schweiz' hinzugefügt.");
            }
            else
            {
                Console.WriteLine("Land 'Schweiz' existiert bereits.");
            }

            // 21. Neuem Restaurant einen Tisch hinzufügen
            Console.WriteLine("21. Tisch zu Restaurant hinzufügen:");
            var irgendeinRestaurant = context.Restaurant.FirstOrDefault();
            if (irgendeinRestaurant != null)
            {
                var neuerTisch = new Table
                {
                    RestaurantId = irgendeinRestaurant.RestaurantId,
                    TableNumber = Guid.NewGuid().ToString().Substring(0, 10),
                    NumberOfSeats = 4
                };
                context.Table.Add(neuerTisch);
                context.SaveChanges();
                Console.WriteLine($"Tisch {neuerTisch.TableNumber} zu {irgendeinRestaurant.Name} hinzugefügt.");
            }
            else
            {
                Console.WriteLine("Kein Restaurant gefunden.");
            }

            //  22. Zeigen Sie alle Reservierungen, die am heutigen Tag (ohne Urzeit) stattfinden
            // und geben Sie diese mit der Uhrzeit aus
            Console.WriteLine("22. Reservierungen, von Heute");
            var heutigeReservierungen = context.Reservation
                .Include(t => t.ReservationTimes)
                .Where(r => r.Date == DateOnly.FromDateTime(DateTime.Today));
            foreach (var r in heutigeReservierungen)
            {
                Console.WriteLine($"{r.Name} reserviert Tisch {r.TableNumber} um {r.ReservationTimes.Time} Uhr.");
            }


            // 23 .Zeigen Sie alle aktiven Tische des Restaurants "La Piazza".

            var tischePiazzaCount = context.Table
               .Include(t => t.Restaurant)
               .Count(t => t.Restaurant.Name == "Musild9ea8f9a-691a-413f-bcd7-0" && t.Activ);

            Console.WriteLine($"Restaurant Musild9ea8f9a-691a-413f-bcd7-0 hat {tischePiazzaCount} Tisch(e).");

            // 24 .Zeigen Sie alle Tische mit ihrem Restaurant
            // und der zugehörigen Kontaktinformation des Restaurants.
            var tablesInRestaurant = context.Table
                .Include(t => t.Restaurant)
                    .ThenInclude(t => t.ContactInfo);

            foreach (var item in tablesInRestaurant)
            {
                Console.WriteLine($"{item.TableNumber} {item.Restaurant.ContactInfo.Email} {item.Restaurant.ContactInfo.PhoneNumber}");
            }

            // 25.  Abfrage, die für ein bestimmtes Restaurant anzeigt, welche Tische am 2025-06-06 um 12:00 Uhr noch frei sind.
            var restaurant = context.Restaurant.First(r=> r.RestaurantId == 6);
            var zeit = context.ReservationTime.First(rt => rt.Time ==   new TimeOnly(12, 00, 0));
            var datum = new DateTime(2025, 6, 6);

            var belegteTischIds = context.Reservation
                .Where(r => r.Date == DateOnly.FromDateTime(datum) && r.ReservationTimeId == zeit.ReservationTimeId)
                .Select(r => r.TableNumber)
                .ToList();

            var freieTische = context.Table
                .Where(t => t.RestaurantId == restaurant.RestaurantId && !belegteTischIds.Contains(t.TableNumber))
                .ToList();

            foreach (var tisch in freieTische)
            {
                Console.WriteLine($"Tisch {tisch.TableNumber}, Sitzplätze: {tisch.NumberOfSeats}");
            }
        }

    }
}
