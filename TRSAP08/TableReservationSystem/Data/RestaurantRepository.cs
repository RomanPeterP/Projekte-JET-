using TableReservationSystem.Models;
using TableReservationSystem.Models.Interfaces;

namespace TableReservationSystem.Data
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly TableReservationSystemContext _context;

        public RestaurantRepository(TableReservationSystemContext context)
        {
            _context = context;
        }

        public void Insert(Restaurant restaurant)
        {
            _context.Restaurant.Add(restaurant);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {

            // TODO: Hier muss man die ganze Kette vorher löschen!
            _context.Restaurant.Remove(_context.Restaurant
                .Where(r => r.RestaurantId == id).FirstOrDefault());
        }

        public bool Any
        {
            get { return _context.Restaurant.Any(); }
        }

        public IEnumerable<IRestaurant> Select
        {
            get { return _context.Restaurant; }
        }
    }
}
