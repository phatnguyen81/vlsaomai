using AutoMapper;
using Nop.Core.Infrastructure;
using Nop.Services.Seo;
using Toi.Plugin.Misc.Articles.Domain;
using Toi.Plugin.Misc.Articles.Models;

namespace Toi.Plugin.Misc.Articles
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            //article group
            Mapper.CreateMap<ArticleGroup, ArticleGroupModel>()
                .ForMember(dest => dest.Locales, mo => mo.Ignore())
                .ForMember(dest => dest.Breadcrumb, mo => mo.Ignore())
                .ForMember(dest => dest.ParentGroups, mo => mo.Ignore());
            Mapper.CreateMap<ArticleGroupModel, ArticleGroup>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.Deleted, mo => mo.Ignore());

            Mapper.CreateMap<Article, ArticleModel>()
                .ForMember(dest => dest.SeName, mo => mo.MapFrom(src => src.GetSeName(0, true, false)))
                .ForMember(dest => dest.StartDate, mo => mo.Ignore())
                .ForMember(dest => dest.EndDate, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            Mapper.CreateMap<ArticleModel, Article>()
                .ForMember(dest => dest.StartDateUtc, mo => mo.Ignore())
                .ForMember(dest => dest.EndDateUtc, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore());
        }

        public int Order { get { return 0; } }
    }
}
