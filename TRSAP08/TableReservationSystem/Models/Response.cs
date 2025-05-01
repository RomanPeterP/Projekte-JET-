using TableReservationSystem.Models.Interfaces;
using static TableReservationSystem.Models.Enums;

namespace TableReservationSystem.Models
{
    public class Response: IResponse
    {
        public StatusCode StatusCode { get; set; }
        public string? Message { get; set; }

        public IEnumerable<IRestaurant>? Data { get; set; }
    }
}
