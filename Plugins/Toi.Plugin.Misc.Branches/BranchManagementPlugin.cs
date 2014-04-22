using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Web;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.Branches.Data;

namespace Toi.Plugin.Misc.Branches
{
    public class BranchManagementPlugin : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {
        private readonly BranchesObjectContext _context;
        private readonly ISettingService _settingService;

        public BranchManagementPlugin(BranchesObjectContext context, 
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
            menuItemBuilder.Text("Branch Management").ImageUrl("~/Administration/Content/images/ico-content.png")
                .Items(a =>
                {
                    a.Add().Text("Branch Group").Url("~/Admin/BranchGroup/List").Text(localizationService.GetResource("Toi.Plugin.Misc.Branches.BranchGroup"));
                    a.Add().Text("Branch").Url("~/Admin/Branch/List").Text(localizationService.GetResource("Toi.Plugin.Misc.Branches.Branch"));
                });
        }

        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch", "Branch");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.List.SearchBranchName","Search Branch Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Title", "Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Short", "Short");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Full", "Full");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.MetaKeywords", "Meta Keywords");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.MetaDescription", "Meta Description");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.MetaTitle", "Meta Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.SeName", "Seo Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Published", "Published");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.CreatedOn", "Created On");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Latitude", "Latitude");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Longitude", "Longitude");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.BranchGroup", "Branch Group");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.AddNew", "Add New");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.EditBranchDetails", "Edit Branch Details");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Manage", "Branch Manage");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.BackToList", "Back to list");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.NoGroupsAvailable", "No Groups Available");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.SaveBeforeEdit", "Save article before edit");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Info", "Info");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Groups", "Groups");


            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.DisplayOrder", "Display Order");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.GroupName", "Group Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Published", "Published");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.IsHotBranch", "Is Hot Branch");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup", "Branch Group");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.List.SearchBranchGroupName", "Search Branch Group Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Name", "Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Description", "Description");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaKeywords", "Meta Keywords");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaDescription", "Meta Description");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaTitle", "Meta Title");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.SeName", "Seo Name");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Parent", "Parent");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.PageSize", "Page Size");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.PageSizeOptions", "Page Size Options");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.ShowOnHomePage", "Show on home page");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Published", "Published");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Deleted", "Deleted");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.DisplayOrder", "Display Order");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.LimitedToStores", "Limited To Stores");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.AvailableStores", "Available Stores");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.AddNew", "Add New");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.EditBranchGroupDetails", "Edit Branch Group Details");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Manage", "Branch Group Manage");

            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Info", "Info");
            this.AddOrUpdatePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.BackToList", "Back to list");
            this.AddOrUpdatePluginLocaleResource("BranchGroups", "Branch Group");
            this.AddOrUpdatePluginLocaleResource("BranchAt", "Branch At");
            
            var branchSettings = new BranchSettings
            {
                BranchThumbPictureSize = 250,
                DefaultBranchGroupId = 1
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
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.List.SearchBranchName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Title");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Short");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Full");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.StartDate");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.EndDate");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.MetaKeywords");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.MetaDescription");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.MetaTitle");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.SeName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Published");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.CreatedOn");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Picture");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Latitude");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.Longitude");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Fields.BranchGroup");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.AddNew");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.EditBranchDetails");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Manage");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.BackToList");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.NoGroupsAvailable");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.SaveBeforeEdit");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Info");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.Branch.Groups");


            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.DisplayOrder");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.GroupName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Published");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.IsHotBranch");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.List.SearchBranchGroupName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Name");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Description");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaKeywords");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaDescription");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaTitle");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.SeName");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Parent");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Picture");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.PageSize");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.PageSizeOptions");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.ShowOnHomePage");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Published");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Deleted");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.DisplayOrder");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.LimitedToStores");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Fields.AvailableStores");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.AddNew");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.EditBranchGroupDetails");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Manage");

            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.Info");
            this.DeletePluginLocaleResource("Toi.Plugin.Misc.Branches.BranchGroup.BackToList");

            this.DeletePluginLocaleResource("BranchGroups");
            this.DeletePluginLocaleResource("BranchAt");

            _settingService.DeleteSetting<BranchSettings>();
            _context.Uninstall();
            base.Uninstall();
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            throw new System.NotImplementedException();
        }
    }
}
