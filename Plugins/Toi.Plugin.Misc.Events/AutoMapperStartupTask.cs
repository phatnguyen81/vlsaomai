using AutoMapper;
using Nop.Core.Infrastructure;
using Toi.Plugin.Misc.Events.Domain;
using Toi.Plugin.Misc.Events.Models;

namespace Toi.Plugin.Misc.Events
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {


            Mapper.CreateMap<Event, EventModel>()
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore());
            Mapper.CreateMap<EventModel, Event>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore());
        }

        public int Order { get { return 0; } }
    }
}
