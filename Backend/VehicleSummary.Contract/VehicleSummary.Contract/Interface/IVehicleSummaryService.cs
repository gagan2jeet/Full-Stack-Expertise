using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleSummary.Common;

namespace VehicleSummary.Contract.Interface
{
    public interface IVehicleSummaryService
    {
        Task<ServiceResponse<VehicleSummaryResponse>> GetSummaryByMake(string make);

        Task<ServiceResponse<VehicleSummaryResponse>> SearchVehicleSummary(string make, string searchString);
    }
}
