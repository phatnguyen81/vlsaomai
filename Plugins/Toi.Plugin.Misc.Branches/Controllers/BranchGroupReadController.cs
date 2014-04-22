using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Admin.Controllers;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Framework.Security;
using Toi.Plugin.Misc.Branches.Cache;
using Toi.Plugin.Misc.Branches.Models;
using Toi.Plugin.Misc.Branches.Services;

namespace Toi.Plugin.Misc.Branches.Controllers
{
    public class BranchGroupReadController : BaseNopController
    {
        #region fields

        private readonly IPermissionService _permissionService;
        private readonly IBranchService _branchService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        private readonly BranchSettings _branchSettings;
        private readonly IWebHelper _webHelper;

        #endregion

        #region ctors
        public BranchGroupReadController(IPermissionService permissionService,
            IBranchService branchService, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            ILocalizedEntityService localizedEntityService, 
            IPictureService pictureService, 
            IUrlRecordService urlRecordService, 
            ICustomerActivityService customerActivityService, 
            IWorkContext workContext,
            ICacheManager cacheManager, 
            BranchSettings branchSettings, 
            IWebHelper webHelper)
        {
            _permissionService = permissionService;
            _branchService = branchService;
            _languageService = languageService;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _pictureService = pictureService;
            _urlRecordService = urlRecordService;
            _customerActivityService = customerActivityService;
            _workContext = workContext;
            _cacheManager = cacheManager;
            _branchSettings = branchSettings;
            _webHelper = webHelper;
        }
        #endregion

        #region Presentation

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Detail(int? id)
        {
            if (id == null || id <= 0)
                return RedirectToAction("Detail", new {id = _branchSettings.DefaultBranchGroupId});
            var branchGroup = _branchService.GetBranchGroupById(id.Value);
            if (branchGroup == null || branchGroup.Deleted)
                return InvokeHttp404();
            var model = branchGroup.ToModel();

            model.Branches =
                _branchService.GetAllBranches(string.Empty, id.Value)
                    .Select(q =>
                    {
                        var branchModel = q.ToModel();
                        branchModel.PictureModel = new PictureModel
                        {
                            FullSizeImageUrl = _pictureService.GetPictureUrl(q.PictureId),
                            ImageUrl = _pictureService.GetPictureUrl(q.PictureId,_branchSettings.BranchThumbPictureSize)
                        };

                        return branchModel;
                    })
                    .ToList();
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult BranchGroupNavigation(int currentBranchGroupId, int currentBranchId)
        {
            //get active category
            var activeBranchGroup = _branchService.GetBranchGroupById(currentBranchGroupId);
            if (activeBranchGroup == null && currentBranchId > 0)
            {
                activeBranchGroup = _branchService.GetBranchGroupByBranchId(currentBranchId);
            }
            var activeBranchGroupId = activeBranchGroup != null ? activeBranchGroup.Id : 0;

            string cacheKey = string.Format(ModelCacheEventConsumer.BRANCHGROUP_NAVIGATION_MODEL_KEY,
                _workContext.WorkingLanguage.Id, activeBranchGroupId);
            var cachedModel = _cacheManager.Get(cacheKey, () =>
            {
                var breadCrumb = activeBranchGroup != null ?
                    activeBranchGroup.GetBranchGroupBreadCrumb(_branchService)
                    .Select(x => x.Id)
                    .ToList()
                    : new List<int>();
                return new BranchGroupNavigationModel()
                {
                    BranchGroups = PrepareBranchGroupNavigationModel(0, breadCrumb).ToList()
                };
            }
            );

            //"CurrentBranchGroupId" property of "BranchGroupNavigationModel" object depends on the current category or product.
            //We need to clone the cached model (the updated one should not be cached)
            var model = (BranchGroupNavigationModel)cachedModel.Clone();
            model.CurrentBranchGroupId = activeBranchGroupId;

            return PartialView(model);
        }
        [NonAction]
        protected IList<BranchGroupNavigationModel.BranchGroupModel> PrepareBranchGroupNavigationModel(int rootBranchGroupId,
            IList<int> breadCrumbIds)
        {
            var result = new List<BranchGroupNavigationModel.BranchGroupModel>();
            foreach (var category in _branchService.GetAllGroupsByParentGroupId(rootBranchGroupId))
            {
                var categoryModel = new BranchGroupNavigationModel.BranchGroupModel()
                {
                    Id = category.Id,
                    Name = category.GetLocalized(x => x.Name),
                    SeName = category.GetSeName()
                };



                //subcategories
                for (int i = 0; i <= breadCrumbIds.Count - 1; i++)
                    if (breadCrumbIds[i] == category.Id)
                        categoryModel.SubBranchGroups.AddRange(PrepareBranchGroupNavigationModel(category.Id, breadCrumbIds));

                result.Add(categoryModel);
            }

            return result;
        }
        #endregion

        public static class ControllerUtilities
        {
            public static IController ResolveNopWebControllerByName(string typeFullName)
            {
                // Since we're not referencing Nop.Web, we must look through the loaded assemblies to find the controller type by name. 
                // Since we know what the type's full name is, we can do a search for it by fullname
                // then resolve its instance from the returned type.
                var assemblies = EngineContext.Current.Resolve<ITypeFinder>().GetAssemblies();

                var controllerType = (from assemply in assemblies
                                      from type in assemply.GetTypes()
                                      where type.FullName.Equals(typeFullName, StringComparison.InvariantCultureIgnoreCase)
                                      select type).FirstOrDefault();

                if (controllerType == null) // Default fallback if the type cannot be found.
                    return null;

                IController controller = EngineContext.Current.Resolve(controllerType) as IController;

                return controller;
            }
        }
        protected virtual ActionResult InvokeHttp404()
        {
            var commonController = ControllerUtilities.ResolveNopWebControllerByName("Nop.Web.Controllers.CommonController");

            if (commonController == null) // Default fallback if the type cannot be found.
                return RedirectToAction("PageNotFound", "Common");

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Common");
            routeData.Values.Add("action", "PageNotFound");

            commonController.Execute(new RequestContext(this.HttpContext, routeData));

            return new EmptyResult();
        }
    }
}
