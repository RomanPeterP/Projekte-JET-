namespace TableReservationSystem.Models.Interfaces
{
    public interface IRestaurantRepository
    {
        bool Any { get; }
        IEnumerable<IRestaurant> Select { get; }

        void Delete(int id);
        void Insert(Restaurant restaurant);

        IEnumerable<IRestaurant> SelectFiltered(string[] words);
    }
}