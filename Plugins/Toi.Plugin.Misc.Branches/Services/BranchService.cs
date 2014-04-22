using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Branches.Domain;

namespace Toi.Plugin.Misc.Branches.Services
{
    public class BranchService : IBranchService
    {
        #region Constants
        private const string GROUPS_BY_ID_KEY = "Toi.branchgroup.id-{0}";
        private const string GROUPS_PATTERN_KEY = "Toi.branchgroup.";
        private const string GROUPS_BY_PARENT_GROUP_ID_KEY = "Toi.branchgroup.byparent-{0}-{1}-{2}";
        #endregion

        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IStoreContext _storeContext;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<BranchGroup> _branchGroupRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public BranchService(IEventPublisher eventPublisher, 
            ILanguageService languageService, 
            IStoreContext storeContext, 
            IRepository<BranchGroup> branchGroupRepository,
            ICacheManager cacheManager,
            IWorkContext workContext, IRepository<Branch> branchRepository)
        {
            this._eventPublisher = eventPublisher;
            _storeContext = storeContext;
            _branchGroupRepository = branchGroupRepository;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _branchRepository = branchRepository;
        }

        #endregion
        #region Branch Group
        public BranchGroup GetBranchGroupById(int groupId)
        {
            if (groupId == 0)
                return null;

            string key = string.Format(GROUPS_BY_ID_KEY, groupId);
            return _cacheManager.Get(key, () => _branchGroupRepository.GetById(groupId));
        }

        public IPagedList<BranchGroup> GetAllBranchGroups(string groupName = "", int pageIndex = 0, int pageSize = Int32.MaxValue)
        {
            var query = _branchGroupRepository.Table;
            if (!String.IsNullOrWhiteSpace(groupName))
                query = query.Where(c => c.Name.Contains(groupName));
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.ParentGroupId).ThenBy(c => c.DisplayOrder);

           
            var unsortedCategories = query.ToList();

            //sort branchGroups
            var sortedCategories = unsortedCategories.SortCategoriesForTree();

            //paging
            return new PagedList<BranchGroup>(sortedCategories, pageIndex, pageSize);
        }

        public void InsertBranchGroup(BranchGroup branchGroup)
        {
            if (branchGroup == null)
                throw new ArgumentNullException("branchGroup");

            _branchGroupRepository.Insert(branchGroup);

            //cache
            _cacheManager.RemoveByPattern(GROUPS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(branchGroup);
        }

        public void UpdateBranchGroup(BranchGroup branchGroup)
        {
            if (branchGroup == null)
                throw new ArgumentNullException("branchGroup");

            //validate branchGroup hierarchy
            var parentBranchGroup = GetBranchGroupById(branchGroup.ParentGroupId);
            while (parentBranchGroup != null)
            {
                if (branchGroup.Id == parentBranchGroup.Id)
                {
                    branchGroup.ParentGroupId = 0;
                    break;
                }
                parentBranchGroup = GetBranchGroupById(parentBranchGroup.ParentGroupId);
            }

            _branchGroupRepository.Update(branchGroup);

            //cache
            _cacheManager.RemoveByPattern(GROUPS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(branchGroup);
        }

        public void DeleteBranchGroup(BranchGroup branchGroup)
        {
            if (branchGroup == null)
                throw new ArgumentNullException("branchGroup");

            branchGroup.Deleted = true;
            UpdateBranchGroup(branchGroup);

            //set a ParentBranchGroup property of the children to 0
            var subbranchGroups = GetAllGroupsByParentGroupId(branchGroup.Id);
            foreach (var subbranchGroup in subbranchGroups)
            {
                subbranchGroup.ParentGroupId = 0;
                UpdateBranchGroup(subbranchGroup);
            }
        }

        public IList<BranchGroup> GetAllGroupsByParentGroupId(int parentGroupId)
        {
            string key = string.Format(GROUPS_BY_PARENT_GROUP_ID_KEY, parentGroupId, _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id);
            return _cacheManager.Get(key, () =>
            {
                var query = _branchGroupRepository.Table;
                query = query.Where(c => c.ParentGroupId == parentGroupId);
                query = query.Where(c => !c.Deleted);
                query = query.OrderBy(c => c.DisplayOrder);

             

                var branchGroups = query.ToList();
                return branchGroups;
            });
        }

        public BranchGroup GetBranchGroupByBranchId(int branchId)
        {
            var branch = GetBranchById(branchId);
            if (branch == null)
                return null;
            var branchGroup = GetBranchGroupById(branch.BranchGroupId);
            return branchGroup;
        }

        #endregion

        #region Branch
        public virtual IPagedList<Branch> GetAllBranches(string title, int branchGroupId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _branchRepository.Table;
            if (!showHidden)
            {
                var utcNow = DateTime.UtcNow;
                query = query.Where(n => n.Published);
            }
            if (branchGroupId > 0)
            {
                query = query.Where(b => b.BranchGroupId == branchGroupId);
            }
            query = query.OrderByDescending(n => n.Id);


            var news = new PagedList<Branch>(query, pageIndex, pageSize);
            return news;
        }

        public void InsertBranch(Branch branch)
        {
            _branchRepository.Insert(branch);
        }

        public void UpdateBranch(Branch branch)
        {
            _branchRepository.Update(branch);
        }

        public Branch GetBranchById(int branchId)
        {
            return _branchRepository.GetById(branchId);
        }





        #endregion
    }
}
