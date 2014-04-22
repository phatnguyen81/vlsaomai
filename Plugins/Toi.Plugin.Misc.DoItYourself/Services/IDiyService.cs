using System.Collections.Generic;
using Nop.Core;
using Toi.Plugin.Misc.DoItYourself.Domain;

namespace Toi.Plugin.Misc.DoItYourself.Services
{
    public interface IDiyService
    {
        #region DiyProject Group
        DiyGroup GetDiyGroupById(int groupId);
        IPagedList<DiyGroup> GetAllDiyGroups(string groupName = "",
           int pageIndex = 0, int pageSize = int.MaxValue);
        void InsertDiyGroup(DiyGroup diyGroup);
        void UpdateDiyGroup(DiyGroup diyGroup);
        void DeleteDiyGroup(DiyGroup diyGroup);
        IList<DiyGroup> GetAllGroupsByParentGroupId(int parentGroupId);

        DiyGroup GetDiyGroupByDiyProjectId(int projectId);
        #endregion

        #region DiyProject

        IPagedList<DiyProject> GetAllDiyProjects(string title = "", int diyGroupId = 0, bool? showOnHomePage = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        void InsertDiyProject(DiyProject diyProject);
        void UpdateDiyProject(DiyProject diyProject);
        DiyProject GetDiyProjectById(int diyProjectId);
        #endregion
    }
}
