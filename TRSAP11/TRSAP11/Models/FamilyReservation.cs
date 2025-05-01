namespace TRSAP11.Models;

public sealed class FamilyReservation : Reservation
{
    public bool IsHighChairRequired { get; set; }
    public override string GetTypeDescription()
    {
        return IsHighChairRequired ? "Reservation mit HighChair" : "Ohne HighChair";
    }
}
