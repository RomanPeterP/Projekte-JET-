
namespace TableReservationSystem.Models.Interfaces
{
    public interface IRestaurantLogic
    {
        public IResponse Register(Restaurant restaurnat);
        public IResponse Data();
    }
}
