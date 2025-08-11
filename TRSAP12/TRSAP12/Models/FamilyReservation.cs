namespace TRSAP12.Models
{
    internal class FamilyReservation: Reservation
    {
        public bool IsHighChairRequired { get; set; }

        public override string GetInfo()
        {
            return "Ich eine Familien-Reservierung";
        }
    }
}
