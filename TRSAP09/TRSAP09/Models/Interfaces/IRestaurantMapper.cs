using TRSAP09.Viewmodels;

namespace TRSAP09.Models.Interfaces
{
    public interface IRestaurantMapper
    {
        RestaurantFormViewModel Map(Restaurant restaurant);
        Restaurant Map(RestaurantFormViewModel vm);
        RestaurantListViewModel ToListViewModel(Restaurant restaurant);
        List<RestaurantListViewModel> ToListViewModels(IEnumerable<Restaurant> restaurants);
    }
}