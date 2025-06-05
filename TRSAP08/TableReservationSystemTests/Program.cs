using Microsoft.EntityFrameworkCore;
using TableReservationSystem.Data;
using TableReservationSystem.Models;

namespace TableReservationSystemTests
{
    internal class Program
    {
        static void Main(string[] args)
        {


            using var context = new TableReservationSystemContext();
            // var liste = context.UpcomingReservation.ToList();

            var date = DateTime.Now;
            var reservierungen = context.Set<ReservationsFromDay>()
                .FromSqlInterpolated($"SELECT * FROM ReservationsFromDay({date})")
                .ToList();

            var anzahl = context.Database
                .SqlQuery<int>($"SELECT dbo.CountOfReservationsFromDay({date})")
                .AsEnumerable().First();


            Modul4TestVorbereitung.Uebungen();
        }
    }
}
