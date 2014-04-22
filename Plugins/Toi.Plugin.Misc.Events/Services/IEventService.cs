using Nop.Core;
using Toi.Plugin.Misc.Events.Domain;

namespace Toi.Plugin.Misc.Events.Services
{
    public interface IEventService
    {
      
        #region Event

        IPagedList<Event> GetAllEvents(string title = "", 
            int pageIndex = 0, int pageSize = int.MaxValue);
        void InsertEvent(Event evt);
        void UpdateEvent(Event evt);
        Event GetEventById(int evtId);

        void DeleteEvent(Event evt);

        #endregion
    }
}
