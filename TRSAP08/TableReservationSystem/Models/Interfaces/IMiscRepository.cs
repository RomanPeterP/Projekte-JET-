namespace TableReservationSystem.Models.Interfaces
{
    public interface IMiscRepository
    {
        IEnumerable<Country> Countries { get; }
    }
}