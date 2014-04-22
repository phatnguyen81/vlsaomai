using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;
using Toi.Plugin.Misc.Services;

namespace Toi.Plugin.Misc.Services
{
    public class DiyManagementRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            ViewEngines.Engines.Add(new CustomViewEngine());

            var lstRoute = new List<Route>();

            lstRoute.Add(routes.MapRoute("Plugin.Misc.Services.Service.Admin",
                "Admin/Service/{action}/{id}",
                new {controller = "Service", action = "Index", id = UrlParameter.Optional},
                new[] {"Toi.Plugin.Misc.Services.Controllers"}
                ));

       
            lstRoute.Add(routes.MapRoute("Services",
                "Services",
                new { controller = "ServiceRead", action = "Index", id = UrlParameter.Optional },
                new[] { "Toi.Plugin.Misc.Services.Controllers" }
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
