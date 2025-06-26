using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            // Aufruf transaktionsgeschützrter gesp.Prozedur
            _context.Database.ExecuteSqlRaw("EXEC DeleteRestaurant @RestaurantId", new SqlParameter("@RestaurantId", id));


            // Vorgangsweise mit EFC(weniger performant) wäre:

            //var contactInfos = _context.Restaurant.Where(r => r.RestaurantId == id).Select(r => r.ContactInfo).ToList();
            //contactInfos.AddRange(_context.Reservation.Where(r => r.RestaurantId == id).Select(s => s.ContactInfo).ToList());

            //_context.Reservation.RemoveRange(_context.Reservation.Where(r => r.RestaurantId == id));
            //_context.Table.RemoveRange(_context.Table.Where(t => t.RestaurantId == id));
            //_context.Restaurant.RemoveRange(_context.Restaurant.Where(r => r.RestaurantId == id));

            //_context.ContactInfo.RemoveRange(contactInfos);

            //_context.SaveChanges();
        }

        public bool Any
        {
            get { return _context.Restaurant.Any(); }
        }

        public IEnumerable<IRestaurant> Select
        {
            get { return _context.Restaurant.AsNoTracking().Include(c => c.ContactInfo); }
        }

        public IEnumerable<IRestaurant> SelectFiltered(string[] words)
        {
            return _context.Restaurant
            .AsNoTracking()
            .Include(r => r.ContactInfo)
            .Where(r =>
                words.All(w =>
                    r.Name.Contains(w) || 
                    r.PostalCode.Contains(w) ||
                    r.City.Contains(w) ||
                    r.StreetHouseNr.Contains(w) ||
                    r.ContactInfo.Email.Contains(w) ||
                    r.ContactInfo.PhoneNumber.Contains(w)
                )
            );
        }
    }
}

