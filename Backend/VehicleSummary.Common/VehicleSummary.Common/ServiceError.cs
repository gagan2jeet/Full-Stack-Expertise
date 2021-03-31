using System.Net;

namespace VehicleSummary.Common
{
    public class ServiceError
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
