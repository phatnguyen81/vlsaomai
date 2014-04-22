using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;
using Toi.Plugin.Misc.Branches;

namespace Toi.Plugin.Misc.Branches
{
    public class BranchManagementRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            ViewEngines.Engines.Add(new CustomViewEngine());

            var lstRoute = new List<Route>();

            lstRoute.Add(routes.MapRoute("Plugin.Misc.Branches.BranchGroup.Admin",
                "Admin/BranchGroup/{action}/{id}",
                new { controller = "BranchGroup", action = "Index", id = UrlParameter.Optional },
                new[] { "Toi.Plugin.Misc.Brances.Controllers" }
                ));
            lstRoute.Add(routes.MapRoute("Plugin.Misc.Branches.Branch.Admin",
                "Admin/Branch/{action}/{id}",
                new {controller = "Branch", action = "Index", id = UrlParameter.Optional},
                new[] {"Toi.Plugin.Misc.Brances.Controllers"}
                ));

            lstRoute.Add(routes.MapRoute("BranchGroup",
                "BranchGroup/{action}/{id}",
                new { controller = "BranchGroupRead", action = "Index", id = UrlParameter.Optional },
                new[] { "Toi.Plugin.Misc.Brances.Controllers" }
                ));

            //lstRoute.Add(routes.MapRoute("BranchGroupNavigation",
            //    "BranchGroupNavigation",
            //    new { controller = "BranchGroup", action = "BranchGroupNavigation", id = UrlParameter.Optional },
            //    new[] { "Toi.Plugin.Misc.Brances.Controllers" }
            //    ));
            foreach (var route in lstRoute)
            {
                routes.Remove(route);
                routes.Insert(0, route);
            }
            //routes.MapRoute("BranchAdmin",
            //      "Admin/Plugins/BranchManagement/BranchAdmin/{action}/{id}",
            //      new { controller = "BranchAdmin", action = "Index", id = UrlParameter.Optional },
            //      new[] { "Nop.Plugin.Other.BranchManagement.Controllers" }
            // ).DataTokens.Add("area", "admin"); ;
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}
