using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Admin.Controllers;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.DoItYourself.Domain;
using Toi.Plugin.Misc.DoItYourself.Models;
using Toi.Plugin.Misc.DoItYourself.Services;

namespace Toi.Plugin.Misc.DoItYourself.Controllers
{
    public class DiyGroupController : BaseNopController
    {
        #region fields

        private readonly IPermissionService _permissionService;
        private readonly IDiyService _diyService;
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
        private readonly DiySettings _diySettings;
        private readonly IWebHelper _webHelper;

        #endregion

        #region ctors
        public DiyGroupController(IPermissionService permissionService,
            IDiyService branchService, 
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
            DiySettings diySettings, 
            IWebHelper webHelper)
        {
            _permissionService = permissionService;
            _diyService = branchService;
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
            _diySettings = diySettings;
            _webHelper = webHelper;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected void UpdateLocales(DiyGroup diyGroup, DiyGroupModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(diyGroup,
                                                               x => x.Name,
                                                               localized.Name,
                                                               localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(diyGroup,
                                                           x => x.Description,
                                                           localized.Description,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(diyGroup,
                                                           x => x.MetaKeywords,
                                                           localized.MetaKeywords,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(diyGroup,
                                                           x => x.MetaDescription,
                                                           localized.MetaDescription,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(diyGroup,
                                                           x => x.MetaTitle,
                                                           localized.MetaTitle,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(diyGroup,
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
            var model = new DiyGroupListModel();
            return View(model);
        }

        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command, DiyGroupListModel model)
        {
            var diyGroups = _diyService.GetAllDiyGroups(model.SearchDiyGroupName,                
                command.Page - 1, command.PageSize);
            var gridModel = new GridModel<DiyGroupModel>
            {
                Data = diyGroups.Select(x =>
                {
                    var categoryModel = x.ToModel();
                    categoryModel.Breadcrumb = x.GetFormattedBreadCrumb(_diyService);
                    return categoryModel;
                }),
                Total = diyGroups.TotalCount
            };
            return new JsonResult
            {
                Data = gridModel
            };
        }

        //ajax
        public ActionResult AllDiyGroups(string text, int selectedId)
        {
            var diyGroups = _diyService.GetAllDiyGroups();
            diyGroups.Insert(0, new DiyGroup { Name = "[None]", Id = 0 });
            var selectList = new List<SelectListItem>();
            foreach (var c in diyGroups)
                selectList.Add(new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.GetFormattedBreadCrumb(_diyService),
                    Selected = c.Id == selectedId
                });

            //var selectList = new SelectList(diyGroups, "Id", "Name", selectedId);
            return new JsonResult { Data = selectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region Create / Edit / Delete

        public ActionResult Create()
        {

            var model = new DiyGroupModel();
            //parent diyGroups
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            //locales
            AddLocales(_languageService, model.Locales);

            return View(model);

        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(DiyGroupModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var diyGroup = model.ToEntity();
                diyGroup.CreatedOnUtc = DateTime.UtcNow;
                diyGroup.UpdatedOnUtc = DateTime.UtcNow;
                _diyService.InsertDiyGroup(diyGroup);
                //search engine name
                model.SeName = diyGroup.ValidateSeName(model.SeName, diyGroup.Name, true);
                _urlRecordService.SaveSlug(diyGroup, model.SeName, 0);
                //locales
                UpdateLocales(diyGroup, model);
                _diyService.UpdateDiyGroup(diyGroup);

                //activity log
                _customerActivityService.InsertActivity("AddNewDiyGroup", _localizationService.GetResource("ActivityLog.AddNewDiyGroup"), diyGroup.Name);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Branches.DiyGroups.Added"));
                return continueEditing
                    ? RedirectToAction("Edit", new { id = diyGroup.Id })
                    : RedirectToAction("List");
            }

            //parent diyGroups
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.ParentGroupId > 0)
            {
                var parentDiyGroup = _diyService.GetDiyGroupById(model.ParentGroupId);
                if (parentDiyGroup != null && !parentDiyGroup.Deleted)
                    model.ParentGroups.Add(new DropDownItem { Text = parentDiyGroup.GetFormattedBreadCrumb(_diyService), Value = parentDiyGroup.Id.ToString() });
                else
                    model.ParentGroupId = 0;
            }
            return View(model);

        }

        public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var diyGroup = _diyService.GetDiyGroupById(id);
            if (diyGroup == null || diyGroup.Deleted)
                //No category found with the specified id
                return RedirectToAction("List");

            var model = diyGroup.ToModel();
            //parent diyGroups
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.ParentGroupId > 0)
            {
                var parentDiyGroup = _diyService.GetDiyGroupById(model.ParentGroupId);
                if (parentDiyGroup != null && !parentDiyGroup.Deleted)
                    model.ParentGroups.Add(new DropDownItem { Text = parentDiyGroup.GetFormattedBreadCrumb(_diyService), Value = parentDiyGroup.Id.ToString() });
                else
                    model.ParentGroupId = 0;
            }
            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Name = diyGroup.GetLocalized(x => x.Name, languageId, false, false);
                locale.Description = diyGroup.GetLocalized(x => x.Description, languageId, false, false);
                locale.MetaKeywords = diyGroup.GetLocalized(x => x.MetaKeywords, languageId, false, false);
                locale.MetaDescription = diyGroup.GetLocalized(x => x.MetaDescription, languageId, false, false);
                locale.MetaTitle = diyGroup.GetLocalized(x => x.MetaTitle, languageId, false, false);
                locale.SeName = diyGroup.GetSeName(languageId, false, false);
            });

            return View(model);

        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(DiyGroupModel model, bool continueEditing)
        {

            var diyGroup = _diyService.GetDiyGroupById(model.Id);
            if (diyGroup == null || diyGroup.Deleted)
                //No category found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                diyGroup = model.ToEntity(diyGroup);
                diyGroup.UpdatedOnUtc = DateTime.UtcNow;
                _diyService.UpdateDiyGroup(diyGroup);
                //search engine name
                model.SeName = diyGroup.ValidateSeName(model.SeName, diyGroup.Name, true);
                _urlRecordService.SaveSlug(diyGroup, model.SeName, 0);
                //locales
                UpdateLocales(diyGroup, model);
                _diyService.UpdateDiyGroup(diyGroup);

                //activity log
                _customerActivityService.InsertActivity("EditDiyGroup", _localizationService.GetResource("ActivityLog.EditDiyGroup"), diyGroup.Name);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Branches.DiyGroups.Updated"));
                return continueEditing
                    ? RedirectToAction("Edit", new { id = diyGroup.Id })
                    : RedirectToAction("List");
            }


            //If we got this far, something failed, redisplay form
            //parent diyGroups
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.ParentGroupId > 0)
            {
                var parentDiyGroup = _diyService.GetDiyGroupById(model.ParentGroupId);
                if (parentDiyGroup != null && !parentDiyGroup.Deleted)
                    model.ParentGroups.Add(new DropDownItem { Text = parentDiyGroup.GetFormattedBreadCrumb(_diyService), Value = parentDiyGroup.Id.ToString() });
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

            var diyGroup = _diyService.GetDiyGroupById(id);
            if (diyGroup == null)
                //No diyGroup found with the specified id
                return RedirectToAction("List");

            _diyService.DeleteDiyGroup(diyGroup);

            //activity log
            _customerActivityService.InsertActivity("DeleteDiyGroup", _localizationService.GetResource("ActivityLog.DeleteDiyGroup"), diyGroup.Name);

            SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Branches.DiyGroups.Deleted"));
            return RedirectToAction("List");
        }


        #endregion

       
    }
}
