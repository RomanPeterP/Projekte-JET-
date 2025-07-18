using System.ComponentModel;
using System.Security.Cryptography;

namespace TableReservationSystem.Viewmodels
{
    public class RestaurantViewModel
    {

        public int RestaurantId { get; set; }

        public string Name { get; set; } = null!;
        [DisplayName("Adresse")]
        public string AddressSummary { get; set; } = null!;
        [DisplayName("E-Mail")]
        public string? Email { get; set; }
        [DisplayName("Telefonnummer")]
        public string? PhoneNumber { get; set; }
        public List<string>? DocsTags
        {
            get
            {
                return new List<string>
            {
                "<img src=\"https://www.rahmenversand.de/media/magefan_blog/2020/10/restaurant-einrichten.jpg.webp\" />" ,
                 "<img src=\"https://www.rahmenversand.de/media/magefan_blog/2020/10/restaurant-einrichten.jpg.webp\" />" ,
                  "<img src=\"https://www.rahmenversand.de/media/magefan_blog/2020/10/restaurant-einrichten.jpg.webp\" />"

            };
            }
        }
    }
}