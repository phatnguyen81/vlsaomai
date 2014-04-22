using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Toi.Plugin.Misc.Branches.Domain;
using Toi.Plugin.Misc.Branches.Models;
using Toi.Plugin.Misc.Branches.Services;

namespace Toi.Plugin.Misc.Branches.Controllers
{
    public class BranchController : BaseNopController
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
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        #region ctors
        public BranchController(IPermissionService permissionService,
            IBranchService branchService, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            ILocalizedEntityService localizedEntityService, 
            IPictureService pictureService, 
            IUrlRecordService urlRecordService, 
            ICustomerActivityService customerActivityService, IDateTimeHelper dateTimeHelper)
        {
            _permissionService = permissionService;
            _branchService = branchService;
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
        protected void UpdateLocales(Branch branch, BranchModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(branch,
                                                               x => x.Title,
                                                               localized.Title,
                                                               localized.LanguageId);


                _localizedEntityService.SaveLocalizedValue(branch,
                                                          x => x.Short,
                                                          localized.MetaKeywords,
                                                          localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(branch,
                                                          x => x.Full,
                                                          localized.MetaKeywords,
                                                          localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(branch,
                                                           x => x.MetaKeywords,
                                                           localized.MetaKeywords,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(branch,
                                                           x => x.MetaDescription,
                                                           localized.MetaDescription,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(branch,
                                                           x => x.MetaTitle,
                                                           localized.MetaTitle,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(branch,
                                                           x => x.SeName,
                                                           localized.SeName,
                                                           localized.LanguageId);
            }
        }
        private void PrepareBranchModel(BranchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("BranchModel");
            }
            model.NumberOfAvailableGroups = _branchService.GetAllBranchGroups().Count;
        }
        #endregion

        #region List
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new BranchListModel();
            
            return View(model);
        }
        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command, BranchListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var branches = _branchService.GetAllBranches(model.SearchBranchName,pageIndex: command.Page - 1,pageSize: command.PageSize,showHidden: true);
            var gridModel = new GridModel<BranchModel>
            {
                Data = branches.Select(x =>
                {
                    var m = x.ToModel();
                    m.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc);
                    return m;
                }),
                Total = branches.TotalCount
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
            var model = new BranchModel();
            AddLocales(_languageService, model.Locales);
            model.Published = true;
            model.BranchGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(BranchModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var branch = model.ToEntity();
                branch.CreatedOnUtc = DateTime.UtcNow;
                _branchService.InsertBranch(branch);

                //search engine name
                model.SeName = branch.ValidateSeName(model.SeName, branch.Title, true);
                _urlRecordService.SaveSlug(branch, model.SeName, 0);

                //locales
                UpdateLocales(branch, model);
                _branchService.UpdateBranch(branch);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Branches.Branch.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = branch.Id }) : RedirectToAction("List");
            }
            //parent branchGroups
            model.BranchGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.BranchGroupId > 0)
            {
                var parentBranchGroup = _branchService.GetBranchGroupById(model.BranchGroupId);
                if (parentBranchGroup != null && !parentBranchGroup.Deleted)
                    model.BranchGroups.Add(new DropDownItem { Text = parentBranchGroup.GetFormattedBreadCrumb(_branchService), Value = parentBranchGroup.Id.ToString() });
                else
                    model.BranchGroupId = 0;
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {

            var branch = _branchService.GetBranchById(id);
            if (branch == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            var model = branch.ToModel();
            PrepareBranchModel(model);

            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Title = branch.GetLocalized(x => x.Title, languageId, false, false);
                locale.Short = branch.GetLocalized(x => x.Short, languageId, false, false);
                locale.Full = branch.GetLocalized(x => x.Full, languageId, false, false);
                locale.MetaKeywords = branch.GetLocalized(x => x.MetaKeywords, languageId, false, false);
                locale.MetaDescription = branch.GetLocalized(x => x.MetaDescription, languageId, false, false);
                locale.MetaTitle = branch.GetLocalized(x => x.MetaTitle, languageId, false, false);
                locale.SeName = branch.GetSeName(languageId, false, false);
            });
            //parent branchGroups
            model.BranchGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.BranchGroupId > 0)
            {
                var parentBranchGroup = _branchService.GetBranchGroupById(model.BranchGroupId);
                if (parentBranchGroup != null && !parentBranchGroup.Deleted)
                    model.BranchGroups.Add(new DropDownItem { Text = parentBranchGroup.GetFormattedBreadCrumb(_branchService), Value = parentBranchGroup.Id.ToString() });
                else
                    model.BranchGroupId = 0;
            }
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormNameAttribute("save-continue", "continueEditing")]
        public ActionResult Edit(BranchModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var branch = _branchService.GetBranchById(model.Id);
            if (branch == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                branch = model.ToEntity(branch);
                _branchService.UpdateBranch(branch);

                //search engine name
                model.SeName = branch.ValidateSeName(model.SeName, branch.Title, true);
                _urlRecordService.SaveSlug(branch, model.SeName, 0);

                //locales
                UpdateLocales(branch, model);
                _branchService.UpdateBranch(branch);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Branches.Branch.Updated"));
                return continueEditing ? RedirectToAction("Edit", new { id = branch.Id }) : RedirectToAction("List");
            }
            PrepareBranchModel(model);
            //parent branchGroups
            model.BranchGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.BranchGroupId > 0)
            {
                var parentBranchGroup = _branchService.GetBranchGroupById(model.BranchGroupId);
                if (parentBranchGroup != null && !parentBranchGroup.Deleted)
                    model.BranchGroups.Add(new DropDownItem { Text = parentBranchGroup.GetFormattedBreadCrumb(_branchService), Value = parentBranchGroup.Id.ToString() });
                else
                    model.BranchGroupId = 0;
            }
            return View(model);
        }
        #endregion

      
    }
}
