using AutoMapper;
using Nop.Core.Infrastructure;
using Toi.Plugin.Misc.Projects.Domain;
using Toi.Plugin.Misc.Projects.Models;

namespace Toi.Plugin.Misc.Projects
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {


            Mapper.CreateMap<Project, ProjectModel>()
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore());
            Mapper.CreateMap<ProjectModel, Project>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore());
        }

        public int Order { get { return 0; } }
    }
}
