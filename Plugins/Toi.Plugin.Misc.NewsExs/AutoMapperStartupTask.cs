using AutoMapper;
using Nop.Core.Infrastructure;
using Toi.Plugin.Misc.NewsExs.Domain;
using Toi.Plugin.Misc.NewsExs.Models;

namespace Toi.Plugin.Misc.NewsExs
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {


            Mapper.CreateMap<NewsEx, NewsExModel>()
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore());
            Mapper.CreateMap<NewsExModel, NewsEx>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore());
        }

        public int Order { get { return 0; } }
    }
}
