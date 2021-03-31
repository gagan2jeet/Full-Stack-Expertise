using Autofac;
using Microsoft.Extensions.Options;
using System.Net.Http;
using VehicleSummary.Business.Services;
using VehicleSummary.Common.Infrastructure;
using VehicleSummary.Contract.Interface;

namespace VehicleSummary.Business
{
    public class VehicleSummaryInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(c => new HttpClient()).As<HttpClient>();
            //Vehicle Summary Service Area
            builder.RegisterType<VehicleSummaryService>().As<IVehicleSummaryService>();
            
        }
    }
}

