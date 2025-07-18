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
    }
}