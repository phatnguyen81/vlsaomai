using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Web;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.DoItYourself.Data;

namespace Toi.Plugin.Misc.DoItYourself
{
    public class DoItYourselfManagementPlugin : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {
        private readonly DoItYourselfObjectContext _context;
        private readonly ISettingService _settingService;

        public DoItYourselfManagementPlugin(DoItYourselfObjectContext context, 
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
            menuItemBuilder.Text("DIY Management").ImageUrl("~/Administration/Content/images/ico-content.png")
                .Items(a =>
                {
                    a.Add().Text("DIY Group").Url("~/Admin/DiyGroup/List").Text(localizationService.GetResource("Toi.Plugin.Misc.DoItYourself.DiyGroup"));
                    a.Add().Text("DIY Project").Url("~/Admin/DiyProject/List").Text(localizationService.GetResource("Toi.Plugin.Misc.DoItYourself.DiyProject"));
                });
        }

        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject", "Diy");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Added", "Add DIY Project successful");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.List.SearchDiyProjectName","Search Project Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Title", "Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Short", "Short");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Full", "Full");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaKeywords", "Meta Keywords");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaDescription", "Meta Description");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaTitle", "Meta Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.SeName", "Seo Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Published", "Published");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.ShowOnHomePage", "Show On Home Page");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.CreatedOn", "Created On");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.DiyGroup", "Diy Group");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.AddNew", "Add New");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.EditDiyDetails", "Edit Diy Details");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Manage", "Diy Manage");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.BackToList", "Back to list");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.NoGroupsAvailable", "No Groups Available");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.SaveBeforeEdit", "Save article before edit");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Info", "Info");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Groups", "Groups");


            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.DisplayOrder", "Display Order");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.GroupName", "Group Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Published", "Published");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.IsHotDiy", "Is Hot Project");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup", "Diy Group");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.List.SearchDiyGroupName", "Search Diy Group Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Name", "Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Description", "Description");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaKeywords", "Meta Keywords");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaDescription", "Meta Description");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaTitle", "Meta Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.SeName", "Seo Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Parent", "Parent");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.PageSize", "Page Size");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.PageSizeOptions", "Page Size Options");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.ShowOnHomePage", "Show on home page");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Published", "Published");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Deleted", "Deleted");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.DisplayOrder", "Display Order");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.LimitedToStores", "Limited To Stores");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.AvailableStores", "Available Stores");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.AddNew", "Add New");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.EditDiyGroupDetails", "Edit Diy Group Details");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Manage", "Diy Group Manage");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Info", "Info");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.BackToList", "Back to list");
            this.AddOrUpdatePluginLocaleResource("DiyGroups", "Diy Group");
            
            var branchSettings = new DiySettings
            {
                DiyProjectThumbPictureSize = 250,
                DefaultDiyGroupId = 1,
                HotestProjectId = 1
            };
            _settingService.SaveSetting(branchSettings);
            
            _context.Install();
            base.Install();
        }
        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Added");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.List.SearchDiyName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Title");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Short");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Full");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.StartDate");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.EndDate");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaKeywords");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaDescription");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaTitle");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.SeName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Published");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.ShowOnHomePage");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.CreatedOn");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Picture");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.DiyGroup");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.AddNew");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.EditDiyDetails");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Manage");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.BackToList");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.NoGroupsAvailable");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.SaveBeforeEdit");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Info");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Groups");


            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.DisplayOrder");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.GroupName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Published");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.IsHotDiy");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.List.SearchDiyGroupName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Name");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Description");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaKeywords");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaDescription");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaTitle");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.SeName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Parent");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Picture");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.PageSize");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.PageSizeOptions");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.ShowOnHomePage");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Published");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Deleted");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.DisplayOrder");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.LimitedToStores");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.AvailableStores");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.AddNew");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.EditDiyGroupDetails");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Manage");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Info");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.BackToList");

            this.DeletePluginLocaleResource("DiyGroups");
            this.DeletePluginLocaleResource("DiyAt");

            _settingService.DeleteSetting<DiySettings>();
            _context.Uninstall();
            base.Uninstall();
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            throw new System.NotImplementedException();
        }
    }
}
