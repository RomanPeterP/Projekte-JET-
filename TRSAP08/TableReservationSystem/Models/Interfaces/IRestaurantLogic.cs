
namespace TableReservationSystem.Models.Interfaces
{
    public interface IRestaurantLogic
    {
        public IResponse<IRestaurant> Register(Restaurant restaurant);
        public IResponse<IRestaurant> Data(RestaurantSearchCriteria? searchKriteria);
        public IResponse<IRestaurant> Delete(Restaurant restaurant);
    }
}
