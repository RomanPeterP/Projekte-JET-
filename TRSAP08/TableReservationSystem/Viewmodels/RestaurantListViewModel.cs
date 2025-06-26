using Microsoft.AspNetCore.Mvc.Rendering;

namespace TableReservationSystem.Viewmodels
{
    public class RestaurantListViewModel: RestaurantBaseViewModel
    {
        public RestaurantSearchCriteriaViewModel? RestaurantSearchCriteriaViewModel { get; set; }
        public IEnumerable<RestaurantViewModel> RestaurantsList { get; set; } = null!;
    }
}
