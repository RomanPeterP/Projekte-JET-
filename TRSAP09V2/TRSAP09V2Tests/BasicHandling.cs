using Microsoft.EntityFrameworkCore;
using RandomNameGeneratorLibrary;
using TRSAP09V2.Data;
using TRSAP09V2.Models;

namespace TRSAP09V2Tests
{
    public static class BasicHandling
	{
		
		public static void SeedData()
		{
			using var context = new Trsap09v2Context();
			try
			{
				context.Database.BeginTransaction();

				SeedCountries(context);
				SeedContactInfos(context);
				SeedRestaurants(context);
				SeedReservationStatus(context);
				SeedReservationTime(context);
				SeedTables(context);
				SeedReservations(context);

				context.Database.CommitTransaction();
			}
			catch 
			{
				context.Database.RollbackTransaction();
				throw;
			}

			
		}

		public static void Clean()
		{
			using var context = new Trsap09v2Context();

			CleanCountries(context);
			CleanContactInfos(context);
			CleanRestaurants(context);
			CleanReservationStatus(context);
			CleanReservationTime(context);
			CleanTables(context);
			CleanReservations(context);

			context.SaveChanges();
		}
		private static void SeedCountries(Trsap09v2Context context)
		{
		
			List<Country> countries =
				[
					new Country{ CountryCode = "AT", Name = "Austria"},
					new Country{ CountryCode = "DE", Name = "Germany"},
					new Country{ CountryCode = "FR", Name = "France"},
					new Country{ CountryCode = "IT", Name = "Italy"},
					new Country{ CountryCode = "ES", Name = "Spain"},
					new Country{ CountryCode = "NL", Name = "Netherlands"},
					new Country{ CountryCode = "BE", Name = "Belgium"},
					new Country{ CountryCode = "CH", Name = "Switzerland"},
					new Country{ CountryCode = "SE", Name = "Sweden"},
					new Country{ CountryCode = "NO", Name = "Norway"},
				];
			
			context.Countries.AddRange(countries);
			context.SaveChanges();
		}
		private static void SeedContactInfos(Trsap09v2Context context)
		{			
			List<ContactInfo> contacts = [
				new ContactInfo{ Email = "alice.johnson@example.com", PhoneNumber = "+1-202-555-0111" },
				new ContactInfo{ Email = "brad.smith@example.net", PhoneNumber = "+44-20-7946-0958" },
				new ContactInfo{ Email = "carlos.mendez@example.org", PhoneNumber = "+34-91-123-4567" },
				new ContactInfo{ Email = "diana.lee@example.com", PhoneNumber = "+61-2-9374-1234" },
				new ContactInfo{ Email = "emily.nguyen@example.net", PhoneNumber = "+1-604-555-0182" },
				new ContactInfo{ Email = "frank.morris@example.org", PhoneNumber = "+49-30-12345678" },
				new ContactInfo{ Email = "grace.tan@example.com", PhoneNumber = "+65-6123-4567" },
				new ContactInfo{ Email = "henry.kim@example.net", PhoneNumber = "+82-2-312-3456" },
				new ContactInfo{ Email = "isabelle.martin@example.org", PhoneNumber = "+33-1-2345-6789" },
				new ContactInfo{ Email = "jack.wilson@example.com", PhoneNumber = "+1-212-555-0173" },
				new ContactInfo{ Email = "karen.cho@example.net", PhoneNumber = "+852-2368-9999" },
				new ContactInfo{ Email = "liam.evans@example.org", PhoneNumber = "+353-1-678-9012" },
				new ContactInfo{ Email = "mia.davies@example.com", PhoneNumber = "+27-11-555-7890" },
				new ContactInfo{ Email = "nathan.zhao@example.net", PhoneNumber = "+86-10-6552-1234" },
				new ContactInfo{ Email = "olivia.brown@example.org", PhoneNumber = "+1-416-555-0147" },
				new ContactInfo{ Email = "paul.torres@example.com", PhoneNumber = "+52-55-1234-5678" },
				new ContactInfo{ Email = "quinn.jackson@example.net", PhoneNumber = "+64-9-123-4567" },
				new ContactInfo{ Email = "rachel.green@example.org", PhoneNumber = "+44-121-555-0100" },
				new ContactInfo{ Email = "samuel.ortega@example.com", PhoneNumber = "+1-305-555-0160" },
				new ContactInfo{ Email = "tina.schmidt@example.net", PhoneNumber = "+49-89-1234567" },
				new ContactInfo{ Email = "ursula.king@example.org", PhoneNumber = "+1-702-555-0199" },
				new ContactInfo{ Email = "victor.chan@example.com", PhoneNumber = "+852-2111-2233" },
				new ContactInfo{ Email = "wendy.hall@example.net", PhoneNumber = "+44-141-555-0188" },
				new ContactInfo{ Email = "xavier.ramirez@example.org", PhoneNumber = "+34-93-987-6543" },
				new ContactInfo{ Email = "yasmin.ali@example.com", PhoneNumber = "+971-4-555-6677" },
				new ContactInfo{ Email = "zachary.hughes@example.net", PhoneNumber = "+1-617-555-0122" },
				new ContactInfo{ Email = "aaron.carter@example.org", PhoneNumber = "+1-312-555-0179" },
				new ContactInfo{ Email = "bella.james@example.com", PhoneNumber = "+61-3-9555-1234" },
				new ContactInfo{ Email = "caleb.liu@example.net", PhoneNumber = "+886-2-2712-3456" },
				new ContactInfo{ Email = "danielle.white@example.org", PhoneNumber = "+1-718-555-0133" },
			];			
			context.AddRange(contacts);
			context.SaveChanges();
		}
		private static void SeedRestaurants(Trsap09v2Context context)
		{
			List<Restaurant> restaurants = 
			[
				new Restaurant { Name = "Alpine Dine", City = "Innsbruck", PostalCode = "6020", StreetHouseNr = "Maria-Theresien-Straße 5", CountryCode = "AT", IsActive = true, ContactInfoId = 1 },
				new Restaurant { Name = "Bierhaus Berlin", City = "Berlin", PostalCode = "10115", StreetHouseNr = "Invalidenstraße 113", CountryCode = "DE", IsActive = true, ContactInfoId = 2 },
				new Restaurant { Name = "Café Provence", City = "Lyon", PostalCode = "69001", StreetHouseNr = "Rue de la République 25", CountryCode = "FR", IsActive = true, ContactInfoId = 3 },
				new Restaurant { Name = "Trattoria Roma", City = "Rome", PostalCode = "00184", StreetHouseNr = "Via Cavour 38", CountryCode = "IT", IsActive = false, ContactInfoId = 4 },
				new Restaurant { Name = "Tapas y Sol", City = "Madrid", PostalCode = "28013", StreetHouseNr = "Calle Mayor 20", CountryCode = "ES", IsActive = true, ContactInfoId = 5 },
				new Restaurant { Name = "Windmill Bistro", City = "Amsterdam", PostalCode = "1012 WX", StreetHouseNr = "Damstraat 10", CountryCode = "NL", IsActive = false, ContactInfoId = 6 },
				new Restaurant { Name = "Belgian Waffles Co.", City = "Brussels", PostalCode = "1000", StreetHouseNr = "Rue Neuve 50", CountryCode = "BE", IsActive = true, ContactInfoId = 7 },
				new Restaurant { Name = "Swiss Fondue House", City = "Zurich", PostalCode = "8001", StreetHouseNr = "Bahnhofstrasse 12", CountryCode = "CH", IsActive = true, ContactInfoId = 8 },
				new Restaurant { Name = "Scandi Flavors", City = "Stockholm", PostalCode = "111 22", StreetHouseNr = "Drottninggatan 45", CountryCode = "SE", IsActive = false, ContactInfoId = 9 },
				new Restaurant { Name = "Fjord Kitchen", City = "Oslo", PostalCode = "0152", StreetHouseNr = "Karl Johans gate 23", CountryCode = "NO", IsActive = true, ContactInfoId = 10 },
				new Restaurant { Name = "Vienna Plates", City = "Vienna", PostalCode = "1010", StreetHouseNr = "Graben 15", CountryCode = "AT", IsActive = false, ContactInfoId = 11 },
				new Restaurant { Name = "Bavarian BBQ", City = "Munich", PostalCode = "80331", StreetHouseNr = "Sendlinger Str. 8", CountryCode = "DE", IsActive = true, ContactInfoId = 12 },
				new Restaurant { Name = "Chez Pierre", City = "Paris", PostalCode = "75001", StreetHouseNr = "Rue Saint-Honoré 101", CountryCode = "FR", IsActive = true, ContactInfoId = 13 },
				new Restaurant { Name = "Pasta Amore", City = "Florence", PostalCode = "50122", StreetHouseNr = "Via dei Neri 7", CountryCode = "IT", IsActive = true, ContactInfoId = 14 },
				new Restaurant { Name = "Paella Paradise", City = "Valencia", PostalCode = "46001", StreetHouseNr = "Calle de la Paz 10", CountryCode = "ES", IsActive = false, ContactInfoId = 15 },
				new Restaurant { Name = "Dutch Delight", City = "Rotterdam", PostalCode = "3011", StreetHouseNr = "Meent 40", CountryCode = "NL", IsActive = true, ContactInfoId = 16 },
				new Restaurant { Name = "Waffle & More", City = "Antwerp", PostalCode = "2000", StreetHouseNr = "Meir 33", CountryCode = "BE", IsActive = true, ContactInfoId = 17 },
				new Restaurant { Name = "Zurich Zest", City = "Zurich", PostalCode = "8005", StreetHouseNr = "Langstrasse 94", CountryCode = "CH", IsActive = false, ContactInfoId = 18 },
				new Restaurant { Name = "Nordic Bites", City = "Gothenburg", PostalCode = "411 03", StreetHouseNr = "Avenyn 12", CountryCode = "SE", IsActive = true, ContactInfoId = 19 },
				new Restaurant { Name = "Oslo Oven", City = "Oslo", PostalCode = "0181", StreetHouseNr = "Storgata 15", CountryCode = "NO", IsActive = true, ContactInfoId = 20 },
				new Restaurant { Name = "Viennese Vibes", City = "Vienna", PostalCode = "1060", StreetHouseNr = "Mariahilfer Straße 66", CountryCode = "AT", IsActive = true, ContactInfoId = 21 },
				new Restaurant { Name = "Schnitzel Stop", City = "Hamburg", PostalCode = "20095", StreetHouseNr = "Spitalerstraße 12", CountryCode = "DE", IsActive = false, ContactInfoId = 22 },
				new Restaurant { Name = "Le Gourmet", City = "Marseille", PostalCode = "13001", StreetHouseNr = "Rue de Rome 22", CountryCode = "FR", IsActive = true, ContactInfoId = 23 },
				new Restaurant { Name = "Sicilian Street Eats", City = "Palermo", PostalCode = "90133", StreetHouseNr = "Via Maqueda 18", CountryCode = "IT", IsActive = true, ContactInfoId = 24 },
				new Restaurant { Name = "Barcelona Bistro", City = "Barcelona", PostalCode = "08002", StreetHouseNr = "La Rambla 25", CountryCode = "ES", IsActive = true, ContactInfoId = 25 },
				new Restaurant { Name = "Canal Café", City = "Utrecht", PostalCode = "3511", StreetHouseNr = "Oudegracht 200", CountryCode = "NL", IsActive = false, ContactInfoId = 26 },
				new Restaurant { Name = "Brussels Kitchen", City = "Brussels", PostalCode = "1050", StreetHouseNr = "Chaussée d'Ixelles 120", CountryCode = "BE", IsActive = true, ContactInfoId = 27 },
				new Restaurant { Name = "Swiss Alps Eatery", City = "Bern", PostalCode = "3011", StreetHouseNr = "Kramgasse 18", CountryCode = "CH", IsActive = false, ContactInfoId = 28 },
				new Restaurant { Name = "Swedish Spoon", City = "Malmö", PostalCode = "211 34", StreetHouseNr = "Södra Förstadsgatan 5", CountryCode = "SE", IsActive = true, ContactInfoId = 29 },
				new Restaurant { Name = "Northern Lights Grill", City = "Bergen", PostalCode = "5003", StreetHouseNr = "Bryggen 1", CountryCode = "NO", IsActive = true, ContactInfoId = 30 },
			];
			
			context.AddRange(restaurants);
			context.SaveChanges();
		}
		private static void SeedReservationStatus(Trsap09v2Context context)
		{
			List <ReservationStatus> status = [
				new ReservationStatus { Caption = "Requested"},
				new ReservationStatus { Caption = "Reserved"},
				new ReservationStatus { Caption = "Cancelled"},
				];

			context.AddRange(status);
			context.SaveChanges();
		}
		private static void SeedReservationTime(Trsap09v2Context context)
		{
			var startTime = new TimeOnly(12, 00);
			var endTime = new TimeOnly(20, 00);
			var currentTime = startTime;
			
			while (currentTime <= endTime)
			{
				context.ReservationTimes.Add(new ReservationTime { Time = currentTime });
				currentTime = currentTime.AddMinutes(15);
			}
			context.SaveChanges();
		}
		private static void SeedTables(Trsap09v2Context context)
		{
			for (int i = 1; i <= 10; i++)
			{
				List<Table> tables = new(40);

				TableNumber('A').ForEach(t => tables.Add(new Table { RestaurantId = i, NumberOfSeats = 4, TableNumber = t, IsActive = true }));
				TableNumber('B').ForEach(t => tables.Add(new Table { RestaurantId = i, NumberOfSeats = 4, TableNumber = t, IsActive = true }));
				TableNumber('C').ForEach(t => tables.Add(new Table { RestaurantId = i, NumberOfSeats = 4, TableNumber = t, IsActive = true }));
				TableNumber('D').ForEach(t => tables.Add(new Table { RestaurantId = i, NumberOfSeats = 4, TableNumber = t, IsActive = true }));

				context.Tables.AddRange(tables);
			}

			for (int i = 11; i <= 28; i++)
			{
				List<Table> tables = new(20);

				TableNumber('A').ForEach(t => tables.Add(new Table { RestaurantId = i, NumberOfSeats = 6, TableNumber = t, IsActive = true }));
				TableNumber('B').ForEach(t => tables.Add(new Table { RestaurantId = i, NumberOfSeats = 6, TableNumber = t, IsActive = true }));

				context.Tables.AddRange(tables);
			}

			context.SaveChanges();
		}
		private static List<string> TableNumber(char tableInit)
		{
			List<string> chars = new List<string>();
			for (int i = 0; i < 10; i++)
			{
				chars.Add(tableInit + i.ToString());
			}
			return chars;
		}

		private static void SeedReservations(Trsap09v2Context context)
		{
			for (int i = 1; i <= 10; i++)
			{
				GenerateReservations(i, 45, context);
			}

			for (int i = 11; i <= 20; i++)
			{
				GenerateReservations(i, 30, context);
			}

			context.SaveChanges();
		}

		private static void GenerateReservations(int resterauntId, int numOfReservations, Trsap09v2Context context)
		{			
			for (int i = 0; i < numOfReservations; i++) {
				var rnd = new Random();
				var personGen = new PersonNameGenerator();
				var firstName = personGen.GenerateRandomFirstName();
				var lastName = personGen.GenerateRandomLastName();
				var email = firstName + "." + lastName + "@example.com";
				var contact = new ContactInfo { Email = email, PhoneNumber = GeneratePhoneNumber() };

				context.ContactInfos.Add(contact);
				context.SaveChanges();

				var contactInfo = context.ContactInfos.First(x => x.Email == email);

				var reservation = new Reservation { 
					Name = firstName + " " + lastName,
					Date = DateOnly.FromDateTime(DateTime.Now.AddDays(rnd.Next(1,14))),
					ContactInfoId = contactInfo.ContactInfoId,
					ReservationTimeId = rnd.Next(1, 33),
					NumberOfGuests = (byte) rnd.Next(1,4),
					RestaurantId = resterauntId,
					ReservationStatusId = 1,
					AdditionalMessage = i%2 == 0 ? "An additional message" : null
				};

				context.Reservations.Add(reservation);
				
			}
		}
		

		private static string GeneratePhoneNumber()
		{
			return "1234567891012";
		}

		private static void CleanCountries(Trsap09v2Context context)
		{
			context.Countries.RemoveRange(context.Countries);
		}
		private static void CleanContactInfos(Trsap09v2Context context)
		{
			context.ContactInfos.RemoveRange(context.ContactInfos);
			ResetTable(context, nameof(ContactInfo));
		}
		private static void CleanRestaurants(Trsap09v2Context context)
		{
			context.Restaurants.RemoveRange(context.Restaurants);
			ResetTable(context, nameof(Restaurant));
		}
		private static void CleanReservationStatus(Trsap09v2Context context)
		{
			context.ReservationStatuses.RemoveRange(context.ReservationStatuses);
			ResetTable(context, nameof(ReservationStatus));
		}		
		private static void CleanReservationTime(Trsap09v2Context context)
		{
			context.ReservationTimes.RemoveRange(context.ReservationTimes);
			ResetTable(context, nameof(ReservationTime));
		}
		private static void CleanTables(Trsap09v2Context context)
		{
			context.Tables.RemoveRange(context.Tables);
			ResetTable(context, nameof(Table));
		}
		private static void CleanReservations(Trsap09v2Context context)
		{
			context.Reservations.RemoveRange(context.Reservations);
			ResetTable(context, nameof(Reservation));
		}
		private static void ResetTable(Trsap09v2Context context, string tableName)
		{
			string sqlResetIdentity = $"DBCC CHECKIDENT ('{tableName}', RESEED, 0)";
			context.Database.ExecuteSqlRaw(sqlResetIdentity);
		}

	}
}
