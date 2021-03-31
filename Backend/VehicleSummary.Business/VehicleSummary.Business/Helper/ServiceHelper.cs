using System;
using VehicleSummary.Common;
using VehicleSummary.Logging.Interface;

namespace VehicleSummary.Business.Helper
{
    public static class ServiceHelper
    {
        public static ServiceResponse<T> CreateResponse<T, D>(IagVehicleAPIResponse<D> iagAPIResponse, Func<D, T> processData, ILoggingHelper logger) where D : new()
        {
            if (iagAPIResponse.Failed)
            {
                return LogErrorAndReturn<T, D>(iagAPIResponse, logger);
            }

            var data = processData(iagAPIResponse.Data);
            return ServiceResponse<T>.WithData(data);
        }

        public static ServiceResponse<T> LogErrorAndReturn<T, D>(IagVehicleAPIResponse<D> iagAPIResponse, ILoggingHelper logger) where D : new()
        {
            var error = iagAPIResponse.Error;
            logger.Log($"Error (API): {error.StatusCode} {error.Message}");
            return ServiceResponse<T>.WithError(error.StatusCode, error.Message);
        }
    }
}
