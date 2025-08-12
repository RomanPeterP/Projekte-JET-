using Microsoft.EntityFrameworkCore;
using TRSAP09V2.Data;
using TRSAP09V2.Models;

namespace TRSAP09V2Tests
{
    public class Modul4TestVorbereitung
    {
        private readonly Trsap09v2Context context;

        public Modul4TestVorbereitung()
        {
            context = new Trsap09v2Context();
        }

        public void Aufgabe01()
        {
            var aktiveRestaurants = context.Restaurants.Where(r => r.IsActive).ToList();
            Console.WriteLine("Aktive Restaurants:");
            foreach (var r in aktiveRestaurants)
                Console.WriteLine($"- {r.Name} ({r.City})");
        }

        public void Aufgabe02()
        {
            var kontakt = context.Restaurants
                .Select(r => r.ContactInfo)
                .FirstOrDefault();
            if (kontakt != null)
                Console.WriteLine($"Kontakt: {kontakt.Email}, {kontakt.PhoneNumber}");
        }

        public void Aufgabe03()
        {
            var ersteKontaktinfo = context.ContactInfos.FirstOrDefault();
            var erstesLand = context.Countries.FirstOrDefault();

            if (ersteKontaktinfo == null || erstesLand == null)
            {
                Console.WriteLine("Fehlende Datenbasis (ContactInfo oder Country).");
                return;
            }

            var neuesRestaurant = new Restaurant
            {
                Name = "Neues Testrestaurant",
                IsActive = true,
                PostalCode = "12345",
                City = "Beispielstadt",
                StreetHouseNr = "Hauptstraße 1",
                CountryCode = erstesLand.CountryCode,
                ContactInfoId = ersteKontaktinfo.ContactInfoId,
                CreatedAt = DateTime.Now
            };

            context.Restaurants.Add(neuesRestaurant);
            context.SaveChanges();
            Console.WriteLine("Neues Restaurant hinzugefügt.");
        }

        public void Aufgabe04()
        {
            var restaurant = context.Restaurants.FirstOrDefault(r => r.IsActive);
            if (restaurant != null)
            {
                restaurant.IsActive = false;
                context.SaveChanges();
                Console.WriteLine($"Restaurant '{restaurant.Name}' deaktiviert.");
            }
            else
            {
                Console.WriteLine("Kein aktives Restaurant gefunden.");
            }
        }

        public void Aufgabe05()
        {
            var restaurant = context.Restaurants.FirstOrDefault();
            var kontakt = context.ContactInfos.FirstOrDefault();
            var zeit = context.ReservationTimes.FirstOrDefault();
            var status = context.ReservationStatuses.FirstOrDefault();

            if (restaurant == null || kontakt == null || zeit == null || status == null)
            {
                Console.WriteLine("Fehlende Stammdaten für Reservierung.");
                return;
            }

            var reservierung = new Reservation
            {
                Name = "Beispielgast",
                Date = DateOnly.FromDateTime(DateTime.Today),
                ReservationTimeId = zeit.ReservationTimeId,
                NumberOfGuests = 2,
                ContactInfoId = kontakt.ContactInfoId,
                RestaurantId = restaurant.RestaurantId,
                ReservationStatusId = status.ReservationStatusId,
                CreatedAt = DateTime.Now
            };

            context.Reservations.Add(reservierung);
            context.SaveChanges();
            Console.WriteLine("Reservierung hinzugefügt.");
        }

        public void Aufgabe06()
        {
            var kontakt = context.ContactInfos
                .Where(c => context.Reservations.Any(r => r.ContactInfoId == c.ContactInfoId))
                .FirstOrDefault();

            if (kontakt == null)
            {
                Console.WriteLine("Kein Kontakt mit Reservierungen gefunden.");
                return;
            }

            var reservierungen = context.Reservations
                .Where(r => r.ContactInfoId == kontakt.ContactInfoId)
                .Select(r => new { r.Name, r.Date })
                .ToList();

            Console.WriteLine($"Reservierungen von Kontakt {kontakt.Email}:");
            foreach (var r in reservierungen)
                Console.WriteLine($"- {r.Name} am {r.Date}");
        }

        public void Aufgabe07()
        {
            var restaurantMitTischen = context.Restaurants
                .Where(r => r.Tables.Any(t => t.IsActive))
                .Include(r => r.Tables)
                .FirstOrDefault();

            if (restaurantMitTischen != null)
            {
                Console.WriteLine($"Aktive Tische in Restaurant '{restaurantMitTischen.Name}':");
                foreach (var t in restaurantMitTischen.Tables.Where(t => t.IsActive))
                    Console.WriteLine("- Tisch " + t.TableNumber);
            }
            else
            {
                Console.WriteLine("Kein Restaurant mit aktiven Tischen gefunden.");
            }
        }

        public void Aufgabe08()
        {
            var resMitRestaurant = context.Reservations
                .Include(r => r.Restaurant)
                .Select(r => new { r.Name, r.Date, RestaurantName = r.Restaurant.Name })
                .ToList();

            Console.WriteLine("Reservierungen inkl. Restaurant:");
            foreach (var r in resMitRestaurant)
                Console.WriteLine($"- {r.Name} am {r.Date} bei {r.RestaurantName}");
        }

        public void Aufgabe09()
        {
            var restaurantsMitTischen = context.Restaurants
                .Include(r => r.Tables.Where(t => t.IsActive))
                .ToList();

            Console.WriteLine("Restaurants mit aktiven Tischen:");
            foreach (var r in restaurantsMitTischen)
            {
                Console.WriteLine($"- {r.Name}:");
                foreach (var t in r.Tables)
                    Console.WriteLine("  - Tisch: " + t.TableNumber);
            }
        }

        public void Aufgabe10()
        {
            var morgen = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            var resHeute = context.Reservations
                .Where(r => r.Date == morgen)
                .Include(r => r.ContactInfo)
                .Select(r => new { r.Name, r.ContactInfo.Email, r.ContactInfo.PhoneNumber })
                .ToList();

            Console.WriteLine("Reservierungen morgen:");
            foreach (var r in resHeute)
                Console.WriteLine($"- {r.Name}: {r.Email}, {r.PhoneNumber}");
        }

        public void Aufgabe11()
        {
            var restaurant = context.Restaurants.FirstOrDefault();
            var zeit = context.ReservationTimes.FirstOrDefault();
            var status = context.ReservationStatuses.FirstOrDefault();

            if (restaurant == null || zeit == null || status == null)
            {
                Console.WriteLine("Fehlende Stammdaten für Reservierung.");
                return;
            }

            var kontakt = new ContactInfo
            {
                Email = "gast@example.com",
                PhoneNumber = "+49 000 000000"
            };

            var reserv = new Reservation
            {
                Name = "Neue Reservierung",
                Date = DateOnly.FromDateTime(DateTime.Today),
                ReservationTimeId = zeit.ReservationTimeId,
                NumberOfGuests = 2,
                ContactInfo = kontakt,
                RestaurantId = restaurant.RestaurantId,
                ReservationStatusId = status.ReservationStatusId,
                CreatedAt = DateTime.Now
            };

            context.Reservations.Add(reserv);
            context.SaveChanges();
            Console.WriteLine("Reservierung mit neuer Kontaktinfo hinzugefügt.");
        }

        public void Aufgabe12()
        {
            var kontakt = context.ContactInfos.FirstOrDefault();
            if (kontakt != null)
            {
                kontakt.PhoneNumber = "+49 123 456789";
                context.SaveChanges();
                Console.WriteLine("Telefonnummer aktualisiert.");
            }
            else
            {
                Console.WriteLine("Keine Kontaktinfo gefunden.");
            }
        }

        public void Aufgabe13()
        {
            var morgen = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

            var timesWithReservations = context.ReservationTimes
                .Include(rt => rt.Reservations)
                .Where(rt => rt.Reservations.Count > 0)
                .ToList();

            Console.WriteLine("Verwendete Reservierungszeiten:");
            foreach (var t in timesWithReservations)
                Console.WriteLine("- " + t.Time);
        }

        public void Aufgabe14()
        {
            var restaurantsMitLand = context.Restaurants
                .Include(r => r.Country)
                .Select(r => new { r.Name, Land = r.Country.Name })
                .ToList();

            Console.WriteLine("Restaurants mit Land:");
            foreach (var r in restaurantsMitLand)
                Console.WriteLine($"- {r.Name} ({r.Land})");
        }

        public void Aufgabe15()
        {
            var cancellationId = context.ReservationStatuses
              .Where(x => x.Caption == "Cancelled")
              .Select(x => x.ReservationStatusId)
              .First();

            context.Reservations
                .Where(x => x.Date < DateOnly.FromDateTime(DateTime.Now))
                .ExecuteUpdate(s => s
                    .SetProperty(p => p.ReservationStatusId, p => cancellationId));
        }

        public void Aufgabe16()
        {
            var statistik = context.Restaurants
                .Select(r => new { r.Name, AnzahlReservierungen = r.Reservations.Count })
                .ToList();

            Console.WriteLine("Reservierungen pro Restaurant:");
            foreach (var r in statistik)
                Console.WriteLine($"- {r.Name}: {r.AnzahlReservierungen}");
        }

        public void Aufgabe17()
        {
            var restaurant = context.Restaurants.FirstOrDefault();
            if (restaurant != null)
            {
                restaurant.Name = "Geänderter Name";
                var entry = context.Entry(restaurant);
                Console.WriteLine("Zustand im ChangeTracker: " + entry.State);
            }
            else
            {
                Console.WriteLine("Kein Restaurant gefunden.");
            }
        }

        public void Aufgabe18()
        {
            var grosseReservierungen = context.Reservations
                .Where(r => r.NumberOfGuests > 2)
                .Select(r => r.Name)
                .ToList();

            Console.WriteLine("Reservierungen mit > 4 Gästen:");
            foreach (var name in grosseReservierungen)
                Console.WriteLine("- " + name);
        }

        public void Aufgabe19()
        {
            var bestesRestaurant = context.Restaurants
                .Select(r => new { r.Name, Anzahl = r.Reservations.Count })
                .OrderByDescending(x => x.Anzahl)
                .FirstOrDefault();

            if (bestesRestaurant != null)
                Console.WriteLine($"Meiste Reservierungen: {bestesRestaurant.Name} ({bestesRestaurant.Anzahl})");
            else
                Console.WriteLine("Keine Reservierungen gefunden.");
        }

        public void Aufgabe20()
        {
            var gruppierung = context.ReservationStatuses
                .Select(rs => new { rs.Caption, Anzahl = rs.Reservations.Count })
                .ToList();

            Console.WriteLine("Reservierungen pro Status:");
            foreach (var g in gruppierung)
                Console.WriteLine($"- {g.Caption}: {g.Anzahl}");
        }

    }

}
