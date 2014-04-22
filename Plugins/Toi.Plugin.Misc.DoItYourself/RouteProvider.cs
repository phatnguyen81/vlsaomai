using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Toi.Plugin.Misc.DoItYourself
{
    public class DiyManagementRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            ViewEngines.Engines.Add(new CustomViewEngine());

            var lstRoute = new List<Route>();

            lstRoute.Add(routes.MapRoute("Plugin.Misc.DoItYourself.DiyGroup.Admin",
                "Admin/DiyGroup/{action}/{id}",
                new { controller = "DiyGroup", action = "Index", id = UrlParameter.Optional },
                new[] { "Toi.Plugin.Misc.DoItYourself.Controllers" }
                ));
            lstRoute.Add(routes.MapRoute("Plugin.Misc.DiyProjectes.DiyProject.Admin",
                "Admin/DiyProject/{action}/{id}",
                new {controller = "DiyProject", action = "Index", id = UrlParameter.Optional},
                new[] {"Toi.Plugin.Misc.DoItYourself.Controllers"}
                ));

            lstRoute.Add(routes.MapRoute("DiyGroup",
                "DiyGroup/{action}/{id}",
                new { controller = "DiyGroupRead", action = "Index", id = UrlParameter.Optional },
                new[] { "Toi.Plugin.Misc.DoItYourself.Controllers" }
                ));

            lstRoute.Add(routes.MapRoute("Diy",
                "DIY",
                new { controller = "DiyGroupRead", action = "Index", id = UrlParameter.Optional },
                new[] { "Toi.Plugin.Misc.DoItYourself.Controllers" }
                ));

            //lstRoute.Add(routes.MapRoute("DiyGroupNavigation",
            //    "DiyGroupNavigation",
            //    new { controller = "DiyGroup", action = "DiyGroupNavigation", id = UrlParameter.Optional },
            //    new[] { "Toi.Plugin.Misc.DoItYourself.Controllers" }
            //    ));
            foreach (var route in lstRoute)
            {
                routes.Remove(route);
                routes.Insert(0, route);
            }
            //routes.MapRoute("DiyProjectAdmin",
            //      "Admin/Plugins/DiyProjectManagement/DiyProjectAdmin/{action}/{id}",
            //      new { controller = "DiyProjectAdmin", action = "Index", id = UrlParameter.Optional },
            //      new[] { "Nop.Plugin.Other.DiyProjectManagement.Controllers" }
            // ).DataTokens.Add("area", "admin"); ;
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}
