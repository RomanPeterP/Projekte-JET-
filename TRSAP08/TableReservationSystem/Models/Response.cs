using TableReservationSystem.Models.Interfaces;
using static TableReservationSystem.Models.Enums;

namespace TableReservationSystem.Models
{
    public class Response<T>: IResponse<T>
    {
        public StatusCode StatusCode { get; set; }
        public string? Message { get; set; }

        public IEnumerable<T>? Data { get; set; }
    }
}
