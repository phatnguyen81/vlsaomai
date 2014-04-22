using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Web.Framework.Web;
using Nop.Services.Localization;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.Articles.Data;

namespace Toi.Plugin.Misc.Articles
{
    public class ArticleManagementPlugin : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {
        private readonly ArticlesObjectContext _context;

        public ArticleManagementPlugin(ArticlesObjectContext context)
        {
            _context = context;
        }

        public bool Authenticate()
        {
            return true;
        }

        public void BuildMenuItem(MenuItemBuilder menuItemBuilder)
        {
            //var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var localizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            menuItemBuilder.Text("Article Management").ImageUrl("~/Administration/Content/images/ico-content.png")
                .Items(a =>
                {
                    a.Add().Text("Article Group").Url("~/Admin/ArticleGroup/List").Text(localizationService.GetResource("Toi.Plugin.Misc.Articles.ArticleGroup"));
                    a.Add().Text("Article").Url("~/Admin/Article/List").Text(localizationService.GetResource("Toi.Plugin.Misc.Articles.Article"));
                });
        }

        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article", "Article");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.List.SearchArticleName","Search Article Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.Title", "Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.Short", "Short");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.Full", "Full");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.StartDate", "Start Date");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.EndDate", "End Date");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.MetaKeywords", "Meta Keywords");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.MetaDescription", "Meta Description");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.MetaTitle", "Meta Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.SeName", "Seo Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.Published", "Published");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.CreatedOn", "Created On");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.Picture", "Picture");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.AddNew", "Add New");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.EditArticleDetails", "Edit Article Details");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Manage", "Article Manage");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.BackToList", "Back to list");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.NoGroupsAvailable", "No Groups Available");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.SaveBeforeEdit", "Save article before edit");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Info", "Info");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Groups", "Groups");


            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.DisplayOrder", "Display Order");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.GroupName", "Group Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Published", "Published");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.IsHotArticle", "Is Hot Article");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup", "Article Group");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.List.SearchArticleGroupName", "Search Article Group Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Name", "Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Description", "Description");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaKeywords", "Meta Keywords");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaDescription", "Meta Description");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaTitle", "Meta Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.SeName", "Seo Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Parent", "Parent");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.PageSize", "Page Size");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.PageSizeOptions", "Page Size Options");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.ShowOnHomePage", "Show on home page");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Published", "Published");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Deleted", "Deleted");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.DisplayOrder", "Display Order");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.LimitedToStores", "Limited To Stores");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.AvailableStores", "Available Stores");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.AddNew", "Add New");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.EditArticleGroupDetails", "Edit Article Group Details");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Manage", "Article Group Manage");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Info", "Info");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.BackToList", "Back to list");
            

            
            _context.Install();
            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.List.SearchArticleName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.Title");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.Short");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.Full");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.StartDate");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.EndDate");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.MetaKeywords");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.MetaDescription");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.MetaTitle");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.SeName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.Published");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.CreatedOn");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Fields.Picture");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.AddNew");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.EditArticleDetails");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Manage");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.BackToList");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.NoGroupsAvailable");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.SaveBeforeEdit");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Info");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.Article.Groups");


            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.DisplayOrder");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.GroupName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Published");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.IsHotArticle");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.List.SearchArticleGroupName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Name");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Description");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaKeywords");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaDescription");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaTitle");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.SeName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Parent");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Picture");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.PageSize");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.PageSizeOptions");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.ShowOnHomePage");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Published");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Deleted");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.DisplayOrder");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.LimitedToStores");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.AvailableStores");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.AddNew");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.EditArticleGroupDetails");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Manage");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.Info");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Articles.ArticleGroup.BackToList");

            _context.Uninstall();
            base.Uninstall();
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            throw new System.NotImplementedException();
        }
    }
}
