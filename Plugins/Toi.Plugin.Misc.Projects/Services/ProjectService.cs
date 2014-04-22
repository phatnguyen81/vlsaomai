using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Projects.Domain;

namespace Toi.Plugin.Misc.Projects.Services
{
    public class ProjectService : IProjectService
    {
        #region Constants
        private const string GROUPS_BY_ID_KEY = "Toi.project.id-{0}";
        private const string GROUPS_PATTERN_KEY = "Toi.project.";
        private const string GROUPS_BY_PARENT_GROUP_ID_KEY = "Toi.project.byparent-{0}-{1}-{2}";
        #endregion

        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IStoreContext _storeContext;
        private readonly IRepository<Project> _projectRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public ProjectService(IEventPublisher eventPublisher, 
            ILanguageService languageService, 
            IStoreContext storeContext, 
            ICacheManager cacheManager,
            IWorkContext workContext, IRepository<Project> projectRepository)
        {
            this._eventPublisher = eventPublisher;
            _storeContext = storeContext;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _projectRepository = projectRepository;
        }

        #endregion
       
        #region Branch
        public virtual IPagedList<Project> GetAllProjects(string title = "",
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _projectRepository.Table;
            query = query.OrderByDescending(n => n.Id);


            var news = new PagedList<Project>(query, pageIndex, pageSize);
            return news;
        }

        public void InsertProject(Project project)
        {
            _projectRepository.Insert(project);
        }

        public void UpdateProject(Project project)
        {
            _projectRepository.Update(project);
        }

        public Project GetProjectById(int projectId)
        {
            return _projectRepository.GetById(projectId);
        }





        #endregion
    }
}
