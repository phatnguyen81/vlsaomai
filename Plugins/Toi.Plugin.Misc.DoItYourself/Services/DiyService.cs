using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;
using Nop.Services.Localization;
using Toi.Plugin.Misc.DoItYourself.Domain;

namespace Toi.Plugin.Misc.DoItYourself.Services
{
    public class DiyService : IDiyService
    {
        #region Constants
        private const string GROUPS_BY_ID_KEY = "Toi.diygroup.id-{0}";
        private const string GROUPS_PATTERN_KEY = "Toi.diygroup.";
        private const string GROUPS_BY_PARENT_GROUP_ID_KEY = "Toi.diygroup.byparent-{0}-{1}-{2}";
        #endregion

        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IStoreContext _storeContext;
        private readonly IRepository<DiyProject> _diyProjectRepository;
        private readonly IRepository<DiyGroup> _diyProjectGroupRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public DiyService(IEventPublisher eventPublisher, 
            ILanguageService languageService, 
            IStoreContext storeContext, 
            IRepository<DiyGroup> diyProjectGroupRepository,
            ICacheManager cacheManager,
            IWorkContext workContext, IRepository<DiyProject> diyProjectRepository)
        {
            this._eventPublisher = eventPublisher;
            _storeContext = storeContext;
            _diyProjectGroupRepository = diyProjectGroupRepository;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _diyProjectRepository = diyProjectRepository;
        }

        #endregion
        #region Branch Group
        public DiyGroup GetDiyGroupById(int groupId)
        {
            if (groupId == 0)
                return null;

            string key = string.Format(GROUPS_BY_ID_KEY, groupId);
            return _cacheManager.Get(key, () => _diyProjectGroupRepository.GetById(groupId));
        }

        public IPagedList<DiyGroup> GetAllDiyGroups(string groupName = "", int pageIndex = 0, int pageSize = Int32.MaxValue)
        {
            var query = _diyProjectGroupRepository.Table;
            if (!String.IsNullOrWhiteSpace(groupName))
                query = query.Where(c => c.Name.Contains(groupName));
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.ParentGroupId).ThenBy(c => c.DisplayOrder);

           
            var unsortedCategories = query.ToList();

            //sort diyProjectGroups
            var sortedCategories = unsortedCategories.SortCategoriesForTree();

            //paging
            return new PagedList<DiyGroup>(sortedCategories, pageIndex, pageSize);
        }

        public void InsertDiyGroup(DiyGroup diyGroup)
        {
            if (diyGroup == null)
                throw new ArgumentNullException("diyGroup");

            _diyProjectGroupRepository.Insert(diyGroup);

            //cache
            _cacheManager.RemoveByPattern(GROUPS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(diyGroup);
        }

        public void UpdateDiyGroup(DiyGroup diyGroup)
        {
            if (diyGroup == null)
                throw new ArgumentNullException("diyGroup");

            //validate diyProjectGroup hierarchy
            var parentDiyGroup = GetDiyGroupById(diyGroup.ParentGroupId);
            while (parentDiyGroup != null)
            {
                if (diyGroup.Id == parentDiyGroup.Id)
                {
                    diyGroup.ParentGroupId = 0;
                    break;
                }
                parentDiyGroup = GetDiyGroupById(parentDiyGroup.ParentGroupId);
            }

            _diyProjectGroupRepository.Update(diyGroup);

            //cache
            _cacheManager.RemoveByPattern(GROUPS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(diyGroup);
        }

        public void DeleteDiyGroup(DiyGroup diyGroup)
        {
            if (diyGroup == null)
                throw new ArgumentNullException("diyGroup");

            diyGroup.Deleted = true;
            UpdateDiyGroup(diyGroup);

            //set a ParentDiyGroup property of the children to 0
            var subdiyProjectGroups = GetAllGroupsByParentGroupId(diyGroup.Id);
            foreach (var subdiyProjectGroup in subdiyProjectGroups)
            {
                subdiyProjectGroup.ParentGroupId = 0;
                UpdateDiyGroup(subdiyProjectGroup);
            }
        }

        public IList<DiyGroup> GetAllGroupsByParentGroupId(int parentGroupId)
        {
            string key = string.Format(GROUPS_BY_PARENT_GROUP_ID_KEY, parentGroupId, _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id);
            return _cacheManager.Get(key, () =>
            {
                var query = _diyProjectGroupRepository.Table;
                query = query.Where(c => c.ParentGroupId == parentGroupId);
                query = query.Where(c => !c.Deleted);
                query = query.OrderBy(c => c.DisplayOrder);

             

                var diyProjectGroups = query.ToList();
                return diyProjectGroups;
            });
        }

        public DiyGroup GetDiyGroupByDiyProjectId(int projectId)
        {
            var diyProject = GetDiyProjectById(projectId);
            if (diyProject == null)
                return null;
            var diyProjectGroup = GetDiyGroupById(diyProject.DiyGroupId);
            return diyProjectGroup;
        }

        #endregion

        #region Branch
        public virtual IPagedList<DiyProject> GetAllDiyProjects(string title = "", int diyGroupId = 0, bool? showOnHomePage = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _diyProjectRepository.Table;
            if (!showHidden)
            {
                var utcNow = DateTime.UtcNow;
                query = query.Where(n => n.Published);
            }
            if (diyGroupId > 0)
            {
                query = query.Where(b => b.DiyGroupId == diyGroupId);
            }
            if (showOnHomePage != null)
            {
                query = query.Where(b => b.ShowOnHomePage == showOnHomePage);
            }
            query = query.OrderByDescending(n => n.Id);


            var news = new PagedList<DiyProject>(query, pageIndex, pageSize);
            return news;
        }

        public void InsertDiyProject(DiyProject diyProject)
        {
            _diyProjectRepository.Insert(diyProject);
        }

        public void UpdateDiyProject(DiyProject diyProject)
        {
            _diyProjectRepository.Update(diyProject);
        }

        public DiyProject GetDiyProjectById(int projectId)
        {
            return _diyProjectRepository.GetById(projectId);
        }





        #endregion
    }
}
