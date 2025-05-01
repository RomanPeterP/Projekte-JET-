using static TRSAP09.Models.Enums;

namespace TRSAP09.Models
{
    public class Response
    {
        public Response(StatusCode statusCode) {
            StatusCode = statusCode;
        }
        public StatusCode StatusCode { get; set; }
        public string? Message { get; set; }

        public IEnumerable<Restaurant>? Data { get; set; }
    }
}
