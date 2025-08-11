namespace TRSAP12.Models
{
    internal class VipReservation: Reservation
    {
        public bool HasWelcomeDrink { get; set; } = true;

        public override string GetInfo()
        {
            return "Ich eine VIP-Reservierung";
        }
    }
}
