using static TRSAP11.Models.Enums;

namespace TRSAP11.Models
{
    public class Response
    {
        public Response(StatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public StatusCode StatusCode { get; set; }
        public string? Message { get; set; }

        public IEnumerable<Restaurant>? Data { get; set; }
    }
}
