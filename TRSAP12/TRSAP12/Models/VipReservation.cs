
namespace TRSAP12.Models
{
    internal class VipReservation: Reservation, IWithSurchage, ISpecialServices
    {
        public bool HasWelcomeDrink { get; set; } = true;
		public decimal? Surcharge { get; set; }
		public IEnumerable<string>? SpecialServices { get; set; }

		public override string GetInfo()
        {
            return "Ich bin eine VIP-Reservierung";
        }
    }
}
