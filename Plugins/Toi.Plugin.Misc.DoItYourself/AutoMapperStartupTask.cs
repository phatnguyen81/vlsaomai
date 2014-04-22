using AutoMapper;
using Nop.Core.Infrastructure;
using Nop.Services.Seo;
using Toi.Plugin.Misc.DoItYourself.Domain;
using Toi.Plugin.Misc.DoItYourself.Models;

namespace Toi.Plugin.Misc.DoItYourself
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            //branch group
            Mapper.CreateMap<DiyGroup, DiyGroupModel>()
                .ForMember(dest => dest.Locales, mo => mo.Ignore())
                .ForMember(dest => dest.Breadcrumb, mo => mo.Ignore())
                .ForMember(dest => dest.ParentGroups, mo => mo.Ignore());
            Mapper.CreateMap<DiyGroupModel, DiyGroup>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.Deleted, mo => mo.Ignore());

            Mapper.CreateMap<DiyProject, DiyProjectModel>()
                .ForMember(dest => dest.SeName, mo => mo.MapFrom(src => src.GetSeName(0, true, false)))
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            Mapper.CreateMap<DiyProjectModel, DiyProject>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore());
        }

        public int Order { get { return 0; } }
    }
}
