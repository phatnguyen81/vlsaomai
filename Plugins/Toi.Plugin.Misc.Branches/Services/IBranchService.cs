using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Toi.Plugin.Misc.Branches.Domain;

namespace Toi.Plugin.Misc.Branches.Services
{
    public interface IBranchService
    {
        #region Branch Group
        BranchGroup GetBranchGroupById(int groupId);
        IPagedList<BranchGroup> GetAllBranchGroups(string groupName = "",
           int pageIndex = 0, int pageSize = int.MaxValue);
        void InsertBranchGroup(BranchGroup branchGroup);
        void UpdateBranchGroup(BranchGroup branchGroup);
        void DeleteBranchGroup(BranchGroup branchGroup);
        IList<BranchGroup> GetAllGroupsByParentGroupId(int parentGroupId);

        BranchGroup GetBranchGroupByBranchId(int branchId);
        #endregion

        #region Branch

        IPagedList<Branch> GetAllBranches(string title, int branchGroupId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        void InsertBranch(Branch article);
        void UpdateBranch(Branch article);
        Branch GetBranchById(int articleId);
        #endregion
    }
}
