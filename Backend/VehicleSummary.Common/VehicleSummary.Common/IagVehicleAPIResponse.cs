using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace VehicleSummary.Common
{
    public class IagVehicleAPIResponse<T> where T : new()
    {
        public T Data { get; set; }
        public IagVehicleAPIError Error { get; set; }

        public bool Succeeded => Error == null;
        public bool Failed => Error != null;

        public static IagVehicleAPIResponse<T> WithData(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return new IagVehicleAPIResponse<T> { Data = new T() };
            }

            return new IagVehicleAPIResponse<T> { Data = JsonConvert.DeserializeObject<T>(data) };
        }

        public static IagVehicleAPIResponse<T> WithError(string error, HttpStatusCode statusCode)
        {
            // Error response from dataverse is of the format:
            // { "error" : { "code": .. , "message": ... } }
            var errorMessage = JObject.Parse(error).SelectToken("error.message").ToString();

            return new IagVehicleAPIResponse<T>
            {
                Error = new IagVehicleAPIError { Message = errorMessage, StatusCode = statusCode }
            };
        }
    }
}
