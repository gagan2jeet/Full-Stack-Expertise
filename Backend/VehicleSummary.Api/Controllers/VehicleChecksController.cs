using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VehicleSummary.Api.Extensions;
using VehicleSummary.Common.Infrastructure;
using VehicleSummary.Contract.Interface;
using VehicleSummary.Logging.Factory;
using VehicleSummary.Logging.Interface;

namespace VehicleSummary.Api.Controllers
{
    [ApiController]
    public class VehicleChecksController : VehicleControllerBase
    {
        private readonly IVehicleSummaryService _vehicleSummaryService;
        private readonly ILoggingHelper logger;
        
        /// <summary>
        /// VehicleChecks controller constructor class
        /// </summary>
        /// <param name="vehicleSummaryService">Vehicle summary service object</param>
        public VehicleChecksController(IVehicleSummaryService vehicleSummaryService)
        {
            _vehicleSummaryService = vehicleSummaryService;
            logger = new LoggingHelperFactory().Manufacture();
        }

        #region Get an vehicle summary by the make

        /// <summary>
        /// Get a vehicle summary details from the API
        /// </summary>
        /// <remarks>
        /// Get vehicle summary by the make 
        /// </remarks>
        /// <param name="make"></param>
        /// <response code="200"></response>

        #endregion Get an vehicle summary by the make
        [HttpGet]
        [Route("/vehicle-checks/makes/{make}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Makes(string make)
        {
            logger.LogAction("Makes controller");
            var response = await _vehicleSummaryService.GetSummaryByMake(make);
            return ProcessResponse(response);
        }

        #region Search an vehicle summary by the model

        /// <summary>
        /// Search model
        /// </summary>
        /// <remarks>
        /// Get a vehicle model details from the make
        /// </remarks>
        /// <param name="make">Enter the Vehicle make</param>
        /// <param name="searchString">Enter the model item to search</param>
        /// <response code="200"></response>

        #endregion Get an vehicle summary by the make
        [HttpGet]
        [Route("/vehicle-checks/search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Search(string make, string searchString)
        {
            logger.LogAction("Search the model");
            var response = await _vehicleSummaryService.SearchVehicleSummary(make, searchString);
            return ProcessResponse(response);
        }
    }
}