using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Toi.Plugin.Misc.NewsExs
{
    public class NewsExsRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            ViewEngines.Engines.Add(new CustomViewEngine());

            var lstRoute = new List<Route>();

            lstRoute.Add(routes.MapRoute("Plugin.Misc.NewsExs.NewsEx.Admin",
                "Admin/NewsEx/{action}/{id}",
                new {controller = "NewsEx", action = "Index", id = UrlParameter.Optional},
                new[] {"Toi.Plugin.Misc.NewsExs.Controllers"}
                ));

       
            lstRoute.Add(routes.MapRoute("NewsExs",
                "NewsExs/{action}/{id}",
                new { controller = "NewsExRead", action = "Index", id = UrlParameter.Optional },
                new[] { "Toi.Plugin.Misc.NewsExs.Controllers" }
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
