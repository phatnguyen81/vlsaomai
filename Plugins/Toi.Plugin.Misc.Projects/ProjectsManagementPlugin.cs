using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Web;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.Projects.Data;

namespace Toi.Plugin.Misc.Projects
{
    public class ProjectsManagementPlugin : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {
        private readonly ProjectsObjectContext _context;
        private readonly ISettingService _settingService;

        public ProjectsManagementPlugin(ProjectsObjectContext context, 
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
            menuItemBuilder.Text("Project Management").ImageUrl("~/Administration/Content/images/ico-content.png")
                .Items(a =>
                {
                    a.Add().Text("Project").Url("~/Admin/Project/List").Text(localizationService.GetResource("Toi.Plugin.Misc.Projects.Project"));
                });
        }

        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project", "Project");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Added", "Add Project successful");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.List.SearchProjectName","Search Project Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.Title", "Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.Title.Required", "Title is required");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.Full", "Full");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.CreatedOn", "Created On");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.DisplayOrder", "DisplayOrder");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.AddNew", "Add New");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.EditProjectDetail", "Edit Project Detail");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Manage", "Projects Manage");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.BackToList", "Back to list");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Info", "Info");


            
            _context.Install();
            base.Install();
        }
        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Added");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.List.SearchProjectName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.Title");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.Title.Required");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.Full");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.CreatedOn");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.Picture");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Fields.DisplayOrder");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.AddNew");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.EditDiyDetails");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Manage");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.BackToList");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Projects.Project.Info");


            _context.Uninstall();
            base.Uninstall();
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            throw new System.NotImplementedException();
        }
    }
}
