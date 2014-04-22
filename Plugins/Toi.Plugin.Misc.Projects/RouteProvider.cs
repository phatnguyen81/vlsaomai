using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;
using Toi.Plugin.Misc.Projects;

namespace Toi.Plugin.Misc.Projects
{
    public class DiyManagementRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            ViewEngines.Engines.Add(new CustomViewEngine());

            var lstRoute = new List<Route>();

            lstRoute.Add(routes.MapRoute("Plugin.Misc.Projects.Project.Admin",
                "Admin/Project/{action}/{id}",
                new {controller = "Project", action = "Index", id = UrlParameter.Optional},
                new[] {"Toi.Plugin.Misc.Projects.Controllers"}
                ));

       
            lstRoute.Add(routes.MapRoute("Projects",
                "Projects",
                new { controller = "ProjectRead", action = "Index", id = UrlParameter.Optional },
                new[] { "Toi.Plugin.Misc.Projects.Controllers" }
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
