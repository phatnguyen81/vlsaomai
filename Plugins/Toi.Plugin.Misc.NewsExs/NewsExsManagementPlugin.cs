using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Web;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.NewsExs.Data;

namespace Toi.Plugin.Misc.NewsExs
{
    public class NewsExsManagementPlugin : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {
        private readonly NewsExsObjectContext _context;
        private readonly ISettingService _settingService;

        public NewsExsManagementPlugin(NewsExsObjectContext context, 
            ISettingService settingService)
        {
            _context = context;
            _settingService = settingService;
        }

        public bool Authenticate()
        {
            return true;
        }

        public void BuildMenuItem(MenuItemBuilder menuItemBuilder)
        {
            //var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var localizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            menuItemBuilder.Text("NewsEx Management").ImageUrl("~/Administration/Content/images/ico-content.png")
                .Items(a =>
                {
                    a.Add().Text("NewsEx").Url("~/Admin/NewsEx/List").Text(localizationService.GetResource("Toi.Plugin.Misc.NewsExs.NewsEx"));
                });
        }

        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx", "NewsEx");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Added", "Add NewsEx successful");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.Detail", "Detail");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.List.SearchNewsExName","Search NewsEx Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Title", "Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Title.Required", "Title is required");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Short", "Short");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Full", "Full");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.CreatedOn", "Created On");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.DisplayOrder", "DisplayOrder");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.AddNew", "Add New");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.EditNewsExDetail", "Edit NewsEx Detail");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Manage", "NewsExs Manage");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.BackToList", "Back to list");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Info", "Info");


            
            _context.Install();
            base.Install();
        }
        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Added");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.List.SearchNewsExName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Title");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Title.Required");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Shore");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Full");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.CreatedOn");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Picture");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.DisplayOrder");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.AddNew");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.EditDiyDetails");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Manage");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.BackToList");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.Detail");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.NewsExs.NewsEx.Info");


            _context.Uninstall();
            base.Uninstall();
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            throw new System.NotImplementedException();
        }
    }
}
