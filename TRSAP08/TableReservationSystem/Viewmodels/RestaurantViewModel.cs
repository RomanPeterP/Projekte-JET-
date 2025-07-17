using System.ComponentModel;

namespace TableReservationSystem.Viewmodels
{
    public class RestaurantViewModel
    {
      
        public int RestaurantId { get; set; }
        [DisplayName("dsdsdds")]
        public string Name { get; set; } = null!;
        public string AddressSummary { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}