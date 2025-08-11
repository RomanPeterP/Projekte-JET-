
namespace TRSAP12.Models
{
    internal class Table
    {
        public string Number { get; set; }
        public byte NumberOfSeats { get; set; }
        public bool Activ { get; set; }
        public bool Outside { get; set; }
        public List<Reservation> Reservation { get; set; }
    }
}
