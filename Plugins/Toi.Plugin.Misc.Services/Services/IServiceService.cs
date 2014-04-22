using System.Collections.Generic;
using Nop.Core;
using Toi.Plugin.Misc.Services.Domain;

namespace Toi.Plugin.Misc.Services.Services
{
    public interface IServiceService
    {
      
        #region Service

        IPagedList<Service> GetAllServices(string title = "", 
            int pageIndex = 0, int pageSize = int.MaxValue);
        void InsertService(Service service);
        void UpdateService(Service service);
        Service GetServiceById(int serviceId);

        void DeleteService(Service service);

        #endregion
    }
}
