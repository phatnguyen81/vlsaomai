using AutoMapper;
using Toi.Plugin.Misc.Events.Domain;
using Toi.Plugin.Misc.Events.Models;

namespace Toi.Plugin.Misc.Events
{
    public static class MappingExtentions
    {
       

        #region Event

        //news items
        public static EventModel ToModel(this Event entity)
        {
            return Mapper.Map<Event, EventModel>(entity);
        }

        public static Event ToEntity(this EventModel model)
        {
            return Mapper.Map<EventModel, Event>(model);
        }

        public static Event ToEntity(this EventModel model, Event destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion
    }
}
