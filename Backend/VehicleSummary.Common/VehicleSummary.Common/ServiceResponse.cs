using System.Net;

namespace VehicleSummary.Common
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public ServiceError Error { get; set; }

        public bool Succeeded => Error == null;

        public static ServiceResponse<T> WithData(T data)
        {
            return new ServiceResponse<T>
            {
                Data = data
            };
        }

        public static ServiceResponse<T> WithError(HttpStatusCode statusCode, string message)
        {
            return new ServiceResponse<T>
            {
                Error = new ServiceError
                {
                    StatusCode = statusCode,
                    Message = message
                }
            };
        }
    }
}

