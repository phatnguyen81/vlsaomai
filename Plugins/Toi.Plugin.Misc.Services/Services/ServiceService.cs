using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Services.Domain;

namespace Toi.Plugin.Misc.Services.Services
{
    public class ServiceService : IServiceService
    {
        #region Constants
        private const string GROUPS_BY_ID_KEY = "Toi.service.id-{0}";
        private const string GROUPS_PATTERN_KEY = "Toi.service .";
        private const string GROUPS_BY_PARENT_GROUP_ID_KEY = "Toi.service .byparent-{0}-{1}-{2}";
        #endregion

        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IStoreContext _storeContext;
        private readonly IRepository<Service> _serviceRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public ServiceService(IEventPublisher eventPublisher, 
            ILanguageService languageService, 
            IStoreContext storeContext, 
            ICacheManager cacheManager,
            IWorkContext workContext, IRepository<Service> serviceRepository)
        {
            this._eventPublisher = eventPublisher;
            _storeContext = storeContext;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _serviceRepository = serviceRepository;
        }

        #endregion
       
        #region Branch
        public virtual IPagedList<Service> GetAllServices(string title = "",
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _serviceRepository.Table;
            query = query.OrderByDescending(n => n.Id);


            var news = new PagedList<Service>(query, pageIndex, pageSize);
            return news;
        }

        public void InsertService(Service service)
        {
            _serviceRepository.Insert(service);
        }

        public void UpdateService(Service service)
        {
            _serviceRepository.Update(service);
        }

        public Service GetServiceById(int serviceId)
        {
            return _serviceRepository.GetById(serviceId);
        }

        public void DeleteService(Service service)
        {
            _serviceRepository.Delete(service);
        }

        #endregion
    }
}
