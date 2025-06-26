using TableReservationSystem.Viewmodels;

namespace TableReservationSystem.Models.Interfaces
{
    public interface IRestaurantMapper
    {
        RestaurantFormViewModel Map(Restaurant? model, string? message);
        Restaurant Map(RestaurantFormViewModel? viewmodel);
        RestaurantListViewModel Map(IEnumerable<IRestaurant>? restaurants, string? message, RestaurantListViewModel? inViemodel);
        RestaurantViewModel Map(IRestaurant? restaurant);
        RestaurantSearchCriteria Map(RestaurantSearchCriteriaViewModel? searchKriteriaViewModel);
    }
}
