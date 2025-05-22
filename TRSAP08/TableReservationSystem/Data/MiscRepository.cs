using Microsoft.EntityFrameworkCore;
using TableReservationSystem.Models;
using TableReservationSystem.Models.Interfaces;

namespace TableReservationSystem.Data
{
    public class MiscRepository : IMiscRepository
    {
        private readonly TableReservationSystemContext _context;

        public MiscRepository(TableReservationSystemContext context)
        {
            _context = context;
        }

        public IEnumerable<Country> Countries
        {
            get { return _context.Country.AsNoTracking(); }
        }
    }
}
