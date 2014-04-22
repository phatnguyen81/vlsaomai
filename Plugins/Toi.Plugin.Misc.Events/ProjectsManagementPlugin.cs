using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Web;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.Events.Data;

namespace Toi.Plugin.Misc.Events
{
    public class EventsManagementPlugin : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {
        private readonly EventsObjectContext _context;
        private readonly ISettingService _settingService;

        public EventsManagementPlugin(EventsObjectContext context, 
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
            menuItemBuilder.Text("Event Management").ImageUrl("~/Administration/Content/images/ico-content.png")
                .Items(a =>
                {
                    a.Add().Text("Event").Url("~/Admin/Event/List").Text(localizationService.GetResource("Toi.Plugin.Misc.Events.Event"));
                });
        }

        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event", "Event");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Added", "Add Event successful");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Detail", "Detail");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.List.SearchEventName","Search Event Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.Title", "Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.Title.Required", "Title is required");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.Short", "Short");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.Full", "Full");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.CreatedOn", "Created On");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.DisplayOrder", "DisplayOrder");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.AddNew", "Add New");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.EditEventDetail", "Edit Event Detail");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Manage", "Events Manage");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.BackToList", "Back to list");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Info", "Info");


            
            _context.Install();
            base.Install();
        }
        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Added");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.List.SearchEventName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.Title");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.Title.Required");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.Shore");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.Full");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.CreatedOn");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.Picture");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Fields.DisplayOrder");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.AddNew");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.EditDiyDetails");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Manage");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.BackToList");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Detail");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Events.Event.Info");


            _context.Uninstall();
            base.Uninstall();
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            throw new System.NotImplementedException();
        }
    }
}
