using System;
using System.Collections.Generic;
using System.Linq;
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
using Nop.Web.Framework.Security;
using Toi.Plugin.Misc.DoItYourself.Cache;
using Toi.Plugin.Misc.DoItYourself.Models;
using Toi.Plugin.Misc.DoItYourself.Services;

namespace Toi.Plugin.Misc.DoItYourself.Controllers
{
    public class DiyGroupReadController : BaseNopController
    {
        #region fields

        private readonly IPermissionService _permissionService;
        private readonly IDiyService _diyService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        private readonly DiySettings _diySettings;
        private readonly IWebHelper _webHelper;

        #endregion

        #region ctors
        public DiyGroupReadController(IPermissionService permissionService,
            IDiyService diyService, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            ILocalizedEntityService localizedEntityService, 
            IPictureService pictureService, 
            IUrlRecordService urlRecordService, 
            ICustomerActivityService customerActivityService, 
            IWorkContext workContext,
            ICacheManager cacheManager, 
            DiySettings diySettings, 
            IWebHelper webHelper)
        {
            _permissionService = permissionService;
            _diyService = diyService;
            _languageService = languageService;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _pictureService = pictureService;
            _urlRecordService = urlRecordService;
            _customerActivityService = customerActivityService;
            _workContext = workContext;
            _cacheManager = cacheManager;
            _diySettings = diySettings;
            _webHelper = webHelper;
        }
        #endregion

        #region Presentation

        public ActionResult Index()
        {
            return View();
        }

       

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Detail(int id)
        {
            if (id <= 0)
                return RedirectToAction("Detail", new {id = _diySettings.DefaultDiyGroupId});
            var branchGroup = _diyService.GetDiyGroupById(id);
            if (branchGroup == null || branchGroup.Deleted)
                return InvokeHttp404();
            var model = branchGroup.ToModel();

            model.DiyProjects =
                _diyService.GetAllDiyProjects(string.Empty, id)
                    .Select(q =>
                    {
                        var branchModel = q.ToModel();
                        branchModel.PictureModel = new PictureModel
                        {
                            FullSizeImageUrl = _pictureService.GetPictureUrl(q.PictureId),
                            ImageUrl = _pictureService.GetPictureUrl(q.PictureId,_diySettings.DiyProjectThumbPictureSize)
                        };

                        return branchModel;
                    })
                    .ToList();
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult DiyGroupNavigation(int currentDiyGroupId, int currentDiyProjectId)
        {
            //get active category
            var activeDiyGroup = _diyService.GetDiyGroupById(currentDiyGroupId);
            if (activeDiyGroup == null && currentDiyProjectId > 0)
            {
                activeDiyGroup = _diyService.GetDiyGroupByDiyProjectId(currentDiyProjectId);
            }
            var activeDiyGroupId = activeDiyGroup != null ? activeDiyGroup.Id : 0;

            string cacheKey = string.Format(ModelCacheEventConsumer.DIYGROUP_NAVIGATION_MODEL_KEY,
                _workContext.WorkingLanguage.Id, activeDiyGroupId);
            var cachedModel = _cacheManager.Get(cacheKey, () => new DiyGroupNavigationModel()
            {
                DiyGroups = PrepareDiyGroupNavigationModel(0).ToList()
            });

            //"CurrentDiyGroupId" property of "DiyGroupNavigationModel" object depends on the current category or product.
            //We need to clone the cached model (the updated one should not be cached)
            var model = (DiyGroupNavigationModel)cachedModel.Clone();
            model.CurrentDiyGroupId = activeDiyGroupId;

            return PartialView(model);
        }
        [NonAction]
        protected IList<DiyGroupNavigationModel.DiyGroupModel> PrepareDiyGroupNavigationModel(int rootDiyGroupId)
        {
            var result = new List<DiyGroupNavigationModel.DiyGroupModel>();
            foreach (var category in _diyService.GetAllGroupsByParentGroupId(rootDiyGroupId))
            {
                var categoryModel = new DiyGroupNavigationModel.DiyGroupModel()
                {
                    Id = category.Id,
                    Name = category.GetLocalized(x => x.Name),
                    SeName = category.GetSeName()
                };



              categoryModel.SubDiyGroups.AddRange(PrepareDiyGroupNavigationModel(category.Id));

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
