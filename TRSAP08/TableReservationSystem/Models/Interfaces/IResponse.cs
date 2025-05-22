using static TableReservationSystem.Models.Enums;

namespace TableReservationSystem.Models.Interfaces
{
    public interface IResponse<T>
    {
        public StatusCode StatusCode { get; set; }
        public string? Message { get; set; }

        public IEnumerable<T>? Data { get; set; }
    }
}
