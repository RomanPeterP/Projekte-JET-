using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Xml;
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
            // Aufruf transaktionsgeschützrter gesp. Prozedur
            _context.Database.ExecuteSqlRaw("EXEC DeleteRestaurant @RestaurantId", new SqlParameter("@RestaurantId", id));


            // Vorgangsweise mit EFC (weniger performant) wäre: 
            var contactInfoId = _context.Reservation.FirstOrDefault(r => r.RestaurantId == id).ContactInfoId;
            _context.Reservation.Remove(_context.Reservation.FirstOrDefault(r => r.RestaurantId == id));
            _context.ContactInfo.Remove(_context.ContactInfo.FirstOrDefault(ci => ci.ContactInfoId == contactInfoId));
            _context.Table.Remove(_context.Table.FirstOrDefault(t => t.RestaurantId == id));
            _context.Restaurant.Remove(_context.Restaurant.FirstOrDefault(r => r.RestaurantId == id));

            _context.SaveChanges();
        }

        public bool Any
        {
            get { return _context.Restaurant.Any(); }
        }

        public IEnumerable<IRestaurant> Select
        {
            get { return _context.Restaurant.Include(c => c.ContactInfo); }
        }
    }
}
