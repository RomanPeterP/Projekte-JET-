using TRSAP11.Models;

namespace TRSAP11.Data
{
    public static class RestaurantData
    {
        static RestaurantData()
        {
            RestaurantsList = new List<Restaurant>();

            var restaurant1 = new Restaurant()
            {
                Name = "Musil",
                ContactInfo = new ContactInfo
                {
                    Email = "musil@musil.at",
                    PhoneNumber = "1234567890"
                },
                PostalCode = "1140",
                City = "Wien",
                StreetHouseNr = "Braillestr. 12",
                Country = Country.AT
            };

            var restaurant2 = new Restaurant()
            {
                Name = "McDonalds",
                ContactInfo = new ContactInfo
                {
                    Email = "4545@dsfdsf.at",
                    PhoneNumber = "54643543"
                },
                PostalCode = "1150",
                City = "Wien",
                StreetHouseNr = "Teststr. 12",
                Country = Country.AT
            };

            var restaurant3 = new Restaurant()
            {
                Name = "Grünspan",
                ContactInfo = new ContactInfo
                {
                    Email = "dfg@dfg.at",
                    PhoneNumber = "65765"
                },
                PostalCode = "1160",
                City = "Wien",
                StreetHouseNr = "Wilhelminenstr. 44",
                Country = Country.AT
            };

            RestaurantsList.AddRange(new [] { restaurant1, restaurant2, restaurant3 });
        }
        public static List<Restaurant>? RestaurantsList { get; set; }
    }
}
