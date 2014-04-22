using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Events.Domain;

namespace Toi.Plugin.Misc.Events.Services
{
    public class EventService : IEventService
    {
        #region Constants
        private const string GROUPS_BY_ID_KEY = "Toi.event.id-{0}";
        private const string GROUPS_PATTERN_KEY = "Toi.event.";
        private const string GROUPS_BY_PARENT_GROUP_ID_KEY = "Toi.event.byparent-{0}-{1}-{2}";
        #endregion

        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IStoreContext _storeContext;
        private readonly IRepository<Event> _diyEventRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public EventService(IEventPublisher eventPublisher, 
            ILanguageService languageService, 
            IStoreContext storeContext, 
            ICacheManager cacheManager,
            IWorkContext workContext, IRepository<Event> diyEventRepository)
        {
            this._eventPublisher = eventPublisher;
            _storeContext = storeContext;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _diyEventRepository = diyEventRepository;
        }

        #endregion
       
        #region Branch
        public virtual IPagedList<Event> GetAllEvents(string title = "",
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _diyEventRepository.Table;
            query = query.OrderByDescending(n => n.Id);


            var news = new PagedList<Event>(query, pageIndex, pageSize);
            return news;
        }

        public void InsertEvent(Event evt)
        {
            _diyEventRepository.Insert(evt);
        }

        public void UpdateEvent(Event evt)
        {
            _diyEventRepository.Update(evt);
        }

        public Event GetEventById(int projectId)
        {
            return _diyEventRepository.GetById(projectId);
        }

        public void DeleteEvent(Event evt)
        {
            _diyEventRepository.Delete(evt);
        }

        #endregion
    }
}
