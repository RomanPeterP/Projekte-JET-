using static TableReservationSystem.Models.Enums;

namespace TableReservationSystem.Models.Interfaces
{
    public interface IResponse
    {
        public StatusCode StatusCode { get; set; }
        public string? Message { get; set; }

        public IEnumerable<IRestaurant>? Data { get; set; }
    }
}
