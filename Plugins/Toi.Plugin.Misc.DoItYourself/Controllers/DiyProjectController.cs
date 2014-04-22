using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Admin.Controllers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Web.Framework.Controllers;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.DoItYourself.Domain;
using Toi.Plugin.Misc.DoItYourself.Models;
using Toi.Plugin.Misc.DoItYourself.Services;

namespace Toi.Plugin.Misc.DoItYourself.Controllers
{
    public class DiyProjectController : BaseNopController
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
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        #region ctors
        public DiyProjectController(IPermissionService permissionService,
            IDiyService diyService, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            ILocalizedEntityService localizedEntityService, 
            IPictureService pictureService, 
            IUrlRecordService urlRecordService, 
            ICustomerActivityService customerActivityService, IDateTimeHelper dateTimeHelper)
        {
            _permissionService = permissionService;
            _diyService = diyService;
            _languageService = languageService;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _pictureService = pictureService;
            _urlRecordService = urlRecordService;
            _customerActivityService = customerActivityService;
            _dateTimeHelper = dateTimeHelper;
        }

        #endregion

        #region Utilitis
        [NonAction]
        protected void UpdateLocales(DiyProject diyProject, DiyProjectModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(diyProject,
                                                               x => x.Title,
                                                               localized.Title,
                                                               localized.LanguageId);


                _localizedEntityService.SaveLocalizedValue(diyProject,
                                                          x => x.Short,
                                                          localized.MetaKeywords,
                                                          localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(diyProject,
                                                          x => x.Full,
                                                          localized.MetaKeywords,
                                                          localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(diyProject,
                                                           x => x.MetaKeywords,
                                                           localized.MetaKeywords,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(diyProject,
                                                           x => x.MetaDescription,
                                                           localized.MetaDescription,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(diyProject,
                                                           x => x.MetaTitle,
                                                           localized.MetaTitle,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(diyProject,
                                                           x => x.SeName,
                                                           localized.SeName,
                                                           localized.LanguageId);
            }
        }
        private void PrepareDiyProjectModel(DiyProjectModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("DiyProjectModel");
            }
            model.NumberOfAvailableGroups = _diyService.GetAllDiyGroups().Count;
        }
        #endregion

        #region List
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new DiyProjectListModel();
            
            return View(model);
        }
        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command, DiyProjectListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var diyProjects = _diyService.GetAllDiyProjects(model.SearchDiyProjectName,pageIndex: command.Page - 1,pageSize: command.PageSize,showHidden: true);
            var gridModel = new GridModel<DiyProjectModel>
            {
                Data = diyProjects.Select(x =>
                {
                    var m = x.ToModel();
                    m.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc);
                    return m;
                }),
                Total = diyProjects.TotalCount
            };
            return new JsonResult
            {
                Data = gridModel
            };
        }
        #endregion

        #region Edit/Create/Delete
        public ActionResult Create()
        {

            ViewBag.AllLanguages = _languageService.GetAllLanguages(true);
            var model = new DiyProjectModel();
            AddLocales(_languageService, model.Locales);
            model.Published = true;
            model.DiyGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(DiyProjectModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var diyProject = model.ToEntity();
                diyProject.CreatedOnUtc = DateTime.UtcNow;
                _diyService.InsertDiyProject(diyProject);

                //search engine name
                model.SeName = diyProject.ValidateSeName(model.SeName, diyProject.Title, true);
                _urlRecordService.SaveSlug(diyProject, model.SeName, 0);

                //locales
                UpdateLocales(diyProject, model);
                _diyService.UpdateDiyProject(diyProject);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = diyProject.Id }) : RedirectToAction("List");
            }
            //parent diyProjectGroups
            model.DiyGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.DiyGroupId > 0)
            {
                var parentBranchGroup = _diyService.GetDiyGroupById(model.DiyGroupId);
                if (parentBranchGroup != null && !parentBranchGroup.Deleted)
                    model.DiyGroups.Add(new DropDownItem { Text = parentBranchGroup.GetFormattedBreadCrumb(_diyService), Value = parentBranchGroup.Id.ToString() });
                else
                    model.DiyGroupId = 0;
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {

            var diyProject = _diyService.GetDiyProjectById(id);
            if (diyProject == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            var model = diyProject.ToModel();
            PrepareDiyProjectModel(model);

            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Title = diyProject.GetLocalized(x => x.Title, languageId, false, false);
                locale.Short = diyProject.GetLocalized(x => x.Short, languageId, false, false);
                locale.Full = diyProject.GetLocalized(x => x.Full, languageId, false, false);
                locale.MetaKeywords = diyProject.GetLocalized(x => x.MetaKeywords, languageId, false, false);
                locale.MetaDescription = diyProject.GetLocalized(x => x.MetaDescription, languageId, false, false);
                locale.MetaTitle = diyProject.GetLocalized(x => x.MetaTitle, languageId, false, false);
                locale.SeName = diyProject.GetSeName(languageId, false, false);
            });
            //parent diyProjectGroups
            model.DiyGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.DiyGroupId > 0)
            {
                var parentBranchGroup = _diyService.GetDiyGroupById(model.DiyGroupId);
                if (parentBranchGroup != null && !parentBranchGroup.Deleted)
                    model.DiyGroups.Add(new DropDownItem { Text = parentBranchGroup.GetFormattedBreadCrumb(_diyService), Value = parentBranchGroup.Id.ToString() });
                else
                    model.DiyGroupId = 0;
            }
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(DiyProjectModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var diyProject = _diyService.GetDiyProjectById(model.Id);
            if (diyProject == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                diyProject = model.ToEntity(diyProject);
                _diyService.UpdateDiyProject(diyProject);

                //search engine name
                model.SeName = diyProject.ValidateSeName(model.SeName, diyProject.Title, true);
                _urlRecordService.SaveSlug(diyProject, model.SeName, 0);

                //locales
                UpdateLocales(diyProject, model);
                _diyService.UpdateDiyProject(diyProject);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Updated"));
                return continueEditing ? RedirectToAction("Edit", new { id = diyProject.Id }) : RedirectToAction("List");
            }
            PrepareDiyProjectModel(model);
            //parent diyProjectGroups
            model.DiyGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.DiyGroupId > 0)
            {
                var parentBranchGroup = _diyService.GetDiyGroupById(model.DiyGroupId);
                if (parentBranchGroup != null && !parentBranchGroup.Deleted)
                    model.DiyGroups.Add(new DropDownItem { Text = parentBranchGroup.GetFormattedBreadCrumb(_diyService), Value = parentBranchGroup.Id.ToString() });
                else
                    model.DiyGroupId = 0;
            }
            return View(model);
        }
        #endregion

      
    }
}
