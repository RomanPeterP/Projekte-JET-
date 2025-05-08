using TableReservationSystem.Viewmodels;

namespace TableReservationSystem.Models.Interfaces
{
    public interface IRestaurantMapper
    {
        RestaurantFormViewModel Map(Restaurant? model, string? message);
        Restaurant Map(RestaurantFormViewModel? viewmodel);
    }
}
