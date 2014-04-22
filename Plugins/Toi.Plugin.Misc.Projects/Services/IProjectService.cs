using System.Collections.Generic;
using Nop.Core;
using Toi.Plugin.Misc.Projects.Domain;

namespace Toi.Plugin.Misc.Projects.Services
{
    public interface IProjectService
    {
      
        #region Project

        IPagedList<Project> GetAllProjects(string title = "", 
            int pageIndex = 0, int pageSize = int.MaxValue);
        void InsertProject(Project project);
        void UpdateProject(Project project);
        Project GetProjectById(int projectId);
        #endregion
    }
}
