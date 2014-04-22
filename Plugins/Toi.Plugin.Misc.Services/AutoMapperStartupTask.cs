using AutoMapper;
using Nop.Core.Infrastructure;
using Toi.Plugin.Misc.Services.Models;
using Toi.Plugin.Misc.Services.Domain;
using Toi.Plugin.Misc.Services.Models;

namespace Toi.Plugin.Misc.Services
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {


            Mapper.CreateMap<Service, ServiceModel>()
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore());
            Mapper.CreateMap<ServiceModel, Service>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore());
        }

        public int Order { get { return 0; } }
    }
}
