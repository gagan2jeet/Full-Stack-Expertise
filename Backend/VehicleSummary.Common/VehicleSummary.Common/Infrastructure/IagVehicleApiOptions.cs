namespace VehicleSummary.Common.Infrastructure
{
    public class IagVehicleApiOptions
    {
        public string GetModelsByMake { get; set; }
        public string GetYearsOfModelsByMake { get; set; }
        public string OcpApimSubscriptionKey { get; set; }
        public string OcpApimSubscriptionValue { get; set; }
    }
}
