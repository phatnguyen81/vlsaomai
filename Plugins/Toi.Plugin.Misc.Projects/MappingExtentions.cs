using AutoMapper;
using Toi.Plugin.Misc.Projects.Domain;
using Toi.Plugin.Misc.Projects.Models;

namespace Toi.Plugin.Misc.Projects
{
    public static class MappingExtentions
    {
       

        #region Project

        //news items
        public static ProjectModel ToModel(this Project entity)
        {
            return Mapper.Map<Project, ProjectModel>(entity);
        }

        public static Project ToEntity(this ProjectModel model)
        {
            return Mapper.Map<ProjectModel, Project>(model);
        }

        public static Project ToEntity(this ProjectModel model, Project destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion
    }
}
