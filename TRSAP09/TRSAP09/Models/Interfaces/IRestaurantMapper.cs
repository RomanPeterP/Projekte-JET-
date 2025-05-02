using TRSAP09.Viewmodels;

namespace TRSAP09.Models.Interfaces
{
    public interface IRestaurantMapper
    {
        RestaurantFormViewModel Map(Restaurant? restaurant);
        Restaurant Map(RestaurantFormViewModel? viewmodel);
        RestaurantListsListViewModel ToListListViewModel(Restaurant? restaurant);
        RestaurantListViewModel ToListViewModels(IEnumerable<Restaurant>? restaurants);
    }
}