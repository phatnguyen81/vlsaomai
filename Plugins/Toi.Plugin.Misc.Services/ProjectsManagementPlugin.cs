using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Web;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.Services.Data;

namespace Toi.Plugin.Misc.Services
{
    public class ServicesManagementPlugin : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {
        private readonly ServicesObjectContext _context;
        private readonly ISettingService _settingService;

        public ServicesManagementPlugin(ServicesObjectContext context, 
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
            menuItemBuilder.Text("Service Management").ImageUrl("~/Administration/Content/images/ico-content.png")
                .Items(a =>
                {
                    a.Add().Text("Service").Url("~/Admin/Service/List").Text(localizationService.GetResource("Toi.Plugin.Misc.Services.Service"));
                });
        }

        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service", "Service");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Added", "Add Service successful");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.List.SearchServiceName","Search Service Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.Title", "Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.Title.Required", "Title is required");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.Full", "Full");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.CreatedOn", "Created On");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.DisplayOrder", "DisplayOrder");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.AddNew", "Add New");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.EditServiceDetail", "Edit Service Detail");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Manage", "Services Manage");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.BackToList", "Back to list");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Info", "Info");


            
            _context.Install();
            base.Install();
        }
        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Added");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.List.SearchServiceName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.Title");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.Title.Required");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.Full");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.CreatedOn");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.Picture");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Fields.DisplayOrder");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.AddNew");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.EditDiyDetails");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Manage");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.BackToList");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Services.Service.Info");


            _context.Uninstall();
            base.Uninstall();
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            throw new System.NotImplementedException();
        }
    }
}
