using TRSAP11.Models.Interfaces;

namespace TRSAP11.Models;

public sealed class VipReservation: Reservation, IWithSurcharge, ISpecialServices
{
    public bool HasWelcomeDrink { get; private set; } = true;
    public decimal? Surcharge { get; set; }
    public IEnumerable<string>? SpecialServices { get; set; }
    public override string GetTypeDescription()
    {
        return Surcharge.HasValue ? $"Surcharge: {Surcharge.Value}" : "Kein Surcharge";
    }
}
