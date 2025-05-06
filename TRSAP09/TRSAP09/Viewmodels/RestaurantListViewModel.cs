namespace TRSAP09.Viewmodels
{
    public class RestaurantListViewModel: RestaurantViewModel
    {
        public IEnumerable<RestaurantListsListViewModel> RestaurantsList { get; set; } = null!;
    }
}