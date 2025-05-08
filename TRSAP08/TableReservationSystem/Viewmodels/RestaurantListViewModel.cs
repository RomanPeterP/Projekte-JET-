namespace TableReservationSystem.Viewmodels
{
    public class RestaurantListViewModel: RestaurantBaseViewModel
    {
        public IEnumerable<RestaurantViewModel> RestaurantsList { get; set; } = null!;
    }
}
