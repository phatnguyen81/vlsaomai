using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Toi.Plugin.Misc.Articles
{
    public class ArticleManagementRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            ViewEngines.Engines.Add(new CustomViewEngine());
            var routeArticleGroupAdmin = routes.MapRoute("Plugin.Misc.Articles.ArticleGroup.Admin",
                "Admin/ArticleGroup/{action}/{id}",
                new { controller = "ArticleGroup", action = "Index", id = UrlParameter.Optional },
                new[] { "Toi.Plugin.Misc.Articles.Controllers" }
                );
            routeArticleGroupAdmin.DataTokens.Add("area", "admin");
            routes.Remove(routeArticleGroupAdmin);
            routes.Insert(0, routeArticleGroupAdmin);

            var routeArticleAdmin = routes.MapRoute("Plugin.Misc.Articles.Article.Admin",
                "Admin/Article/{action}/{id}",
                new { controller = "Article", action = "Index", id = UrlParameter.Optional },
                new[] { "Toi.Plugin.Misc.Articles.Controllers" }
                );
            routeArticleAdmin.DataTokens.Add("area", "admin");
            routes.Remove(routeArticleAdmin);
            routes.Insert(0, routeArticleAdmin);
            //routes.MapRoute("ArticleAdmin",
            //      "Admin/Plugins/ArticleManagement/ArticleAdmin/{action}/{id}",
            //      new { controller = "ArticleAdmin", action = "Index", id = UrlParameter.Optional },
            //      new[] { "Nop.Plugin.Other.ArticleManagement.Controllers" }
            // ).DataTokens.Add("area", "admin"); ;
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}
