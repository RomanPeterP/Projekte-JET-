namespace TRSAP09.Viewmodels
{
    public class RestaurantListsListViewModel
    {
        public int RestaurantId { get; set; }

        public string Name { get; set; } = null!;

        public string AddressSummary { get; set; } = null!;

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public bool Activ { get; set; }
    }
}
