using System.Net;

namespace VehicleSummary.Common
{
    public class IagVehicleAPIError
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}