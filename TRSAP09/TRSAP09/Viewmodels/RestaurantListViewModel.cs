namespace TRSAP09.Viewmodels
{
    public class RestaurantListViewModel
    {
        public IEnumerable<RestaurantListsListViewModel> RestaurantsList { get; set; } = null!;
        public string? Message { get; set; }
    }
}