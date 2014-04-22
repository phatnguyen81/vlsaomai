using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Toi.Plugin.Misc.Events
{
    public class EventsRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            ViewEngines.Engines.Add(new CustomViewEngine());

            var lstRoute = new List<Route>();

            lstRoute.Add(routes.MapRoute("Plugin.Misc.Events.Event.Admin",
                "Admin/Event/{action}/{id}",
                new {controller = "Event", action = "Index", id = UrlParameter.Optional},
                new[] {"Toi.Plugin.Misc.Events.Controllers"}
                ));

       
            lstRoute.Add(routes.MapRoute("Events",
                "Events/{action}/{id}",
                new { controller = "EventRead", action = "Index", id = UrlParameter.Optional },
                new[] { "Toi.Plugin.Misc.Events.Controllers" }
                ));


            foreach (var route in lstRoute)
            {
                routes.Remove(route);
                routes.Insert(0, route);
            }

        }

        public int Priority
        {
            get { return 0; }
        }
    }
}
