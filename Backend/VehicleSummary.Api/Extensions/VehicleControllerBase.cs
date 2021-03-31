using Microsoft.AspNetCore.Mvc;
using VehicleSummary.Common;

namespace VehicleSummary.Api.Extensions
{
    public class VehicleControllerBase : ControllerBase
    {
        /// <summary>
        /// Return the ServiceResponse so that all our JSON responses have a structured format
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceResponse"></param>
        /// <returns></returns>
        internal IActionResult ProcessResponse<T>(ServiceResponse<T> serviceResponse)
        {
            if (serviceResponse.Succeeded)
            {
                return Ok(serviceResponse);
            }

            return StatusCode((int)serviceResponse.Error.StatusCode, serviceResponse);
        }
    }
}
