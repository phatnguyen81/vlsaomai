using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Admin.Controllers;
using Nop.Admin.Infrastructure.Cache;
using Nop.Admin.Models.Catalog;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.Branches.Domain;
using Toi.Plugin.Misc.Branches.Models;
using Toi.Plugin.Misc.Branches.Services;
using ModelCacheEventConsumer = Toi.Plugin.Misc.Branches.Cache.ModelCacheEventConsumer;

namespace Toi.Plugin.Misc.Branches.Controllers
{
    public class BranchGroupController : BaseNopController
    {
        #region fields

        private readonly IPermissionService _permissionService;
        private readonly IBranchService _branchService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IStoreService _storeService;
        private readonly IPictureService _pictureService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        private readonly BranchSettings _branchSettings;
        private readonly IWebHelper _webHelper;

        #endregion

        #region ctors
        public BranchGroupController(IPermissionService permissionService,
            IBranchService branchService, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            IStoreMappingService storeMappingService, 
            ILocalizedEntityService localizedEntityService, 
            IStoreService storeService,
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
            _storeMappingService = storeMappingService;
            _localizedEntityService = localizedEntityService;
            _storeService = storeService;
            _pictureService = pictureService;
            _urlRecordService = urlRecordService;
            _customerActivityService = customerActivityService;
            _workContext = workContext;
            _cacheManager = cacheManager;
            _branchSettings = branchSettings;
            _webHelper = webHelper;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected void UpdateLocales(BranchGroup branchGroup, BranchGroupModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(branchGroup,
                                                               x => x.Name,
                                                               localized.Name,
                                                               localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(branchGroup,
                                                           x => x.Description,
                                                           localized.Description,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(branchGroup,
                                                           x => x.MetaKeywords,
                                                           localized.MetaKeywords,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(branchGroup,
                                                           x => x.MetaDescription,
                                                           localized.MetaDescription,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(branchGroup,
                                                           x => x.MetaTitle,
                                                           localized.MetaTitle,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(branchGroup,
                                                           x => x.SeName,
                                                           localized.SeName,
                                                           localized.LanguageId);
            }
        }



        #endregion

        #region List / tree
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new BranchGroupListModel();
            return View(model);
        }

        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command, BranchGroupListModel model)
        {
            var branchGroups = _branchService.GetAllBranchGroups(model.SearchBranchGroupName,                
                command.Page - 1, command.PageSize);
            var gridModel = new GridModel<BranchGroupModel>
            {
                Data = branchGroups.Select(x =>
                {
                    var categoryModel = x.ToModel();
                    categoryModel.Breadcrumb = x.GetFormattedBreadCrumb(_branchService);
                    return categoryModel;
                }),
                Total = branchGroups.TotalCount
            };
            return new JsonResult
            {
                Data = gridModel
            };
        }

        //ajax
        public ActionResult AllBranchGroups(string text, int selectedId)
        {
            var branchGroups = _branchService.GetAllBranchGroups();
            branchGroups.Insert(0, new BranchGroup { Name = "[None]", Id = 0 });
            var selectList = new List<SelectListItem>();
            foreach (var c in branchGroups)
                selectList.Add(new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.GetFormattedBreadCrumb(_branchService),
                    Selected = c.Id == selectedId
                });

            //var selectList = new SelectList(branchGroups, "Id", "Name", selectedId);
            return new JsonResult { Data = selectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region Create / Edit / Delete

        public ActionResult Create()
        {

            var model = new BranchGroupModel();
            //parent branchGroups
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            //locales
            AddLocales(_languageService, model.Locales);

            return View(model);

        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(BranchGroupModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var branchGroup = model.ToEntity();
                branchGroup.CreatedOnUtc = DateTime.UtcNow;
                branchGroup.UpdatedOnUtc = DateTime.UtcNow;
                _branchService.InsertBranchGroup(branchGroup);
                //search engine name
                model.SeName = branchGroup.ValidateSeName(model.SeName, branchGroup.Name, true);
                _urlRecordService.SaveSlug(branchGroup, model.SeName, 0);
                //locales
                UpdateLocales(branchGroup, model);
                _branchService.UpdateBranchGroup(branchGroup);

                //activity log
                _customerActivityService.InsertActivity("AddNewBranchGroup", _localizationService.GetResource("ActivityLog.AddNewBranchGroup"), branchGroup.Name);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Branches.BranchGroups.Added"));
                return continueEditing
                    ? RedirectToAction("Edit", new { id = branchGroup.Id })
                    : RedirectToAction("List");
            }

            //parent branchGroups
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.ParentGroupId > 0)
            {
                var parentBranchGroup = _branchService.GetBranchGroupById(model.ParentGroupId);
                if (parentBranchGroup != null && !parentBranchGroup.Deleted)
                    model.ParentGroups.Add(new DropDownItem { Text = parentBranchGroup.GetFormattedBreadCrumb(_branchService), Value = parentBranchGroup.Id.ToString() });
                else
                    model.ParentGroupId = 0;
            }
            return View(model);

        }

        public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var branchGroup = _branchService.GetBranchGroupById(id);
            if (branchGroup == null || branchGroup.Deleted)
                //No category found with the specified id
                return RedirectToAction("List");

            var model = branchGroup.ToModel();
            //parent branchGroups
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.ParentGroupId > 0)
            {
                var parentBranchGroup = _branchService.GetBranchGroupById(model.ParentGroupId);
                if (parentBranchGroup != null && !parentBranchGroup.Deleted)
                    model.ParentGroups.Add(new DropDownItem { Text = parentBranchGroup.GetFormattedBreadCrumb(_branchService), Value = parentBranchGroup.Id.ToString() });
                else
                    model.ParentGroupId = 0;
            }
            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Name = branchGroup.GetLocalized(x => x.Name, languageId, false, false);
                locale.Description = branchGroup.GetLocalized(x => x.Description, languageId, false, false);
                locale.MetaKeywords = branchGroup.GetLocalized(x => x.MetaKeywords, languageId, false, false);
                locale.MetaDescription = branchGroup.GetLocalized(x => x.MetaDescription, languageId, false, false);
                locale.MetaTitle = branchGroup.GetLocalized(x => x.MetaTitle, languageId, false, false);
                locale.SeName = branchGroup.GetSeName(languageId, false, false);
            });

            return View(model);

        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(BranchGroupModel model, bool continueEditing)
        {

            var branchGroup = _branchService.GetBranchGroupById(model.Id);
            if (branchGroup == null || branchGroup.Deleted)
                //No category found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                branchGroup = model.ToEntity(branchGroup);
                branchGroup.UpdatedOnUtc = DateTime.UtcNow;
                _branchService.UpdateBranchGroup(branchGroup);
                //search engine name
                model.SeName = branchGroup.ValidateSeName(model.SeName, branchGroup.Name, true);
                _urlRecordService.SaveSlug(branchGroup, model.SeName, 0);
                //locales
                UpdateLocales(branchGroup, model);
                _branchService.UpdateBranchGroup(branchGroup);

                //activity log
                _customerActivityService.InsertActivity("EditBranchGroup", _localizationService.GetResource("ActivityLog.EditBranchGroup"), branchGroup.Name);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Branches.BranchGroups.Updated"));
                return continueEditing
                    ? RedirectToAction("Edit", new { id = branchGroup.Id })
                    : RedirectToAction("List");
            }


            //If we got this far, something failed, redisplay form
            //parent branchGroups
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.ParentGroupId > 0)
            {
                var parentBranchGroup = _branchService.GetBranchGroupById(model.ParentGroupId);
                if (parentBranchGroup != null && !parentBranchGroup.Deleted)
                    model.ParentGroups.Add(new DropDownItem { Text = parentBranchGroup.GetFormattedBreadCrumb(_branchService), Value = parentBranchGroup.Id.ToString() });
                else
                    model.ParentGroupId = 0;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var branchGroup = _branchService.GetBranchGroupById(id);
            if (branchGroup == null)
                //No branchGroup found with the specified id
                return RedirectToAction("List");

            _branchService.DeleteBranchGroup(branchGroup);

            //activity log
            _customerActivityService.InsertActivity("DeleteBranchGroup", _localizationService.GetResource("ActivityLog.DeleteBranchGroup"), branchGroup.Name);

            SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Branches.BranchGroups.Deleted"));
            return RedirectToAction("List");
        }


        #endregion

       
    }
}
