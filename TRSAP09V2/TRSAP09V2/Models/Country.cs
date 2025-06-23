namespace TRSAP09V2.Models;

public class Country
{
    public string CountryCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
