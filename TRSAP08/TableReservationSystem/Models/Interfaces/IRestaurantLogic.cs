
namespace TableReservationSystem.Models.Interfaces
{
    public interface IRestaurantLogic
    {
        public IResponse Register(Restaurant restaurant);
        public IResponse Data();
    }
}
