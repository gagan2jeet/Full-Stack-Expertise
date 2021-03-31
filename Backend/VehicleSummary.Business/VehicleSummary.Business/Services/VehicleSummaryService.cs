using Microsoft.Extensions.Options;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VehicleSummary.Business.Helper;
using VehicleSummary.Common;
using VehicleSummary.Common.Infrastructure;
using VehicleSummary.Contract.Interface;
using VehicleSummary.Logging.Factory;
using VehicleSummary.Logging.Interface;

namespace VehicleSummary.Business.Services
{
    public class VehicleSummaryService : IVehicleSummaryService
    {
        private readonly ILoggingHelper _logger;
        private readonly HttpClient _client;
        private readonly IagVehicleApiOptions _iagVehicleAPIOptions;
        public VehicleSummaryService(HttpClient client, IOptions<IagVehicleApiOptions> iagVehicleAPIOptions)
        {
            _client = client;
            _logger = new LoggingHelperFactory().Manufacture();
            _iagVehicleAPIOptions = iagVehicleAPIOptions.Value;
        }

        public async Task<ServiceResponse<VehicleSummaryResponse>> GetSummaryByMake(string make)
        {
            VehicleSummaryResponse vehicleSummaryResponse = new VehicleSummaryResponse
            {
                Make = make,
                Models = new System.Collections.Generic.List<VehicleSummaryModels>()
            };
            HttpResponseMessage response = null;
            IagVehicleAPIResponse<VehicleSummaryResponse> vehicleSummaryModelsRefObj = null;

            try
            {
                _client.DefaultRequestHeaders.Add(_iagVehicleAPIOptions.OcpApimSubscriptionKey, _iagVehicleAPIOptions.OcpApimSubscriptionValue);
                response = await _client.GetAsync(string.Format(_iagVehicleAPIOptions.GetModelsByMake, make));
                var iagResponseData = await ProcessResponse<JArray>(response);

                foreach (var model in iagResponseData.Data)
                {
                    VehicleSummaryModels models = new VehicleSummaryModels();
                    models.Name = model.ToString();
                    response = await _client.GetAsync(string.Format(_iagVehicleAPIOptions.GetModelsByMake, make, model));
                    var vehicleSummaryModelsRef = await ProcessResponse<JArray>(response);
                    models.YearsAvailable = vehicleSummaryModelsRef.Data.Count;
                    vehicleSummaryResponse.Models.Add(models);
                }
                vehicleSummaryModelsRefObj = await ProcessSummaryResponse<VehicleSummaryResponse>(vehicleSummaryResponse, true, response);
            }
            catch (Exception)
            {
                vehicleSummaryModelsRefObj = await ProcessSummaryResponse<VehicleSummaryResponse>(null, false, response);
            }

            return ServiceHelper.CreateResponse(vehicleSummaryModelsRefObj, (data) => data, _logger);
        }

        public async Task<ServiceResponse<VehicleSummaryResponse>> SearchVehicleSummary(string make, string searchString)
        {
            VehicleSummaryResponse vehicleSummaryResponse = new VehicleSummaryResponse
            {
                Make = make,
                Models = new System.Collections.Generic.List<VehicleSummaryModels>()
            };
            HttpResponseMessage response = null;
            IagVehicleAPIResponse<VehicleSummaryResponse> vehicleSummaryModelsRefObj = null;
            try
            {
                _client.DefaultRequestHeaders.Add(_iagVehicleAPIOptions.OcpApimSubscriptionKey, _iagVehicleAPIOptions.OcpApimSubscriptionValue);
                response = await _client.GetAsync(string.Format(_iagVehicleAPIOptions.GetModelsByMake, make));
                var iagResponseData = await ProcessResponse<JArray>(response);
                // Get the search on the data
                dynamic searchResponseData;
                if (!string.IsNullOrEmpty(searchString))
                {
                    searchResponseData = iagResponseData.Data.Select(x => x).ToArray().Where(x => x.ToString().Contains(searchString));
                }
                else
                {
                    searchResponseData = iagResponseData.Data.Select(x => x);
                }
                foreach (var model in searchResponseData)
                {
                    VehicleSummaryModels models = new VehicleSummaryModels
                    {
                        Name = model.ToString()
                    };
                    response = await _client.GetAsync(string.Format(_iagVehicleAPIOptions.GetYearsOfModelsByMake, make, model));
                    var vehicleSummaryModelsRef = await ProcessResponse<JArray>(response);
                    models.YearsAvailable = vehicleSummaryModelsRef.Data.Count;
                    vehicleSummaryResponse.Models.Add(models);
                }
                vehicleSummaryModelsRefObj = await ProcessSummaryResponse<VehicleSummaryResponse>(vehicleSummaryResponse, true, response);
            }
            catch (Exception)
            {
                vehicleSummaryModelsRefObj = await ProcessSummaryResponse<VehicleSummaryResponse>(null, false, response);
            }

            return ServiceHelper.CreateResponse(vehicleSummaryModelsRefObj, (data) => data, _logger);
        }

        private async Task<IagVehicleAPIResponse<T>> ProcessResponse<T>(HttpResponseMessage response) where T : new()
        {
            string responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return IagVehicleAPIResponse<T>.WithData(responseBody);
            }

            return IagVehicleAPIResponse<T>.WithError(responseBody, response.StatusCode);
        }

        private async Task<IagVehicleAPIResponse<T>> ProcessSummaryResponse<T>(VehicleSummaryResponse summaryResponse, bool validResponse, HttpResponseMessage response) where T : new()
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            if (validResponse)
            {
                return IagVehicleAPIResponse<T>.WithData(new JavaScriptSerializer().Serialize(summaryResponse));
            }

            return IagVehicleAPIResponse<T>.WithError(responseBody, response.StatusCode);
        }
    }
}
