using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;
using Nop.Services.Localization;
using Toi.Plugin.Misc.NewsExs.Domain;

namespace Toi.Plugin.Misc.NewsExs.Services
{
    public class NewsExService : INewsExService
    {
        #region Constants
        private const string GROUPS_BY_ID_KEY = "Toi.newsEx.id-{0}";
        private const string GROUPS_PATTERN_KEY = "Toi.newsEx.";
        private const string GROUPS_BY_PARENT_GROUP_ID_KEY = "Toi.newsEx.byparent-{0}-{1}-{2}";
        #endregion

        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IStoreContext _storeContext;
        private readonly IRepository<NewsEx> _diyNewsExRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public NewsExService(IEventPublisher eventPublisher, 
            ILanguageService languageService, 
            IStoreContext storeContext, 
            ICacheManager cacheManager,
            IWorkContext workContext, IRepository<NewsEx> diyNewsExRepository)
        {
            this._eventPublisher = eventPublisher;
            _storeContext = storeContext;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _diyNewsExRepository = diyNewsExRepository;
        }

        #endregion
       
        #region Branch
        public virtual IPagedList<NewsEx> GetAllNewsExs(string title = "",
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _diyNewsExRepository.Table;
            query = query.OrderByDescending(n => n.Id);


            var news = new PagedList<NewsEx>(query, pageIndex, pageSize);
            return news;
        }

        public void InsertNewsEx(NewsEx newsEx)
        {
            _diyNewsExRepository.Insert(newsEx);
        }

        public void UpdateNewsEx(NewsEx newsEx)
        {
            _diyNewsExRepository.Update(newsEx);
        }

        public NewsEx GetNewsExById(int projectId)
        {
            return _diyNewsExRepository.GetById(projectId);
        }

        public void DeleteService(NewsEx newsEx)
        {
            _diyNewsExRepository.Delete(newsEx);
        }

        #endregion
    }
}
