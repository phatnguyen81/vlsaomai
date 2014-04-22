using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toi.Plugin.Misc.Events.Models;

namespace Toi.Plugin.Misc.Events.Models
{
    public class EventIndexModel
    {
        public EventIndexModel()
        {
            PagingFilteringContext = new EventsPagingFilteringModel();
        }
        public List<EventModel> Events { get; set; }

        public EventsPagingFilteringModel PagingFilteringContext { get; set; }
    }
}
