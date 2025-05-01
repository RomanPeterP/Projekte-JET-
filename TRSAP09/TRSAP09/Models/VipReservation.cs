using TRSAP09.Models.Interfaces;

namespace TRSAP09.Models;

public sealed class VipReservation: Reservation, IWithSurcharge, ISpecialServices
{
    public bool HasWelcomeDrink { get; private set; } = true;
    public decimal? Surcharge { get; set; }
    public IEnumerable<string>? SpecialServices { get; set; }
}
