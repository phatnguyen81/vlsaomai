using AutoMapper;
using Nop.Core.Infrastructure;
using Nop.Services.Seo;
using Toi.Plugin.Misc.Branches.Domain;
using Toi.Plugin.Misc.Branches.Models;

namespace Toi.Plugin.Misc.Branches
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            //branch group
            Mapper.CreateMap<BranchGroup, BranchGroupModel>()
                .ForMember(dest => dest.Locales, mo => mo.Ignore())
                .ForMember(dest => dest.Breadcrumb, mo => mo.Ignore())
                .ForMember(dest => dest.ParentGroups, mo => mo.Ignore());
            Mapper.CreateMap<BranchGroupModel, BranchGroup>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.Deleted, mo => mo.Ignore());

            Mapper.CreateMap<Branch, BranchModel>()
                .ForMember(dest => dest.SeName, mo => mo.MapFrom(src => src.GetSeName(0, true, false)))
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            Mapper.CreateMap<BranchModel, Branch>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore());
        }

        public int Order { get { return 0; } }
    }
}
