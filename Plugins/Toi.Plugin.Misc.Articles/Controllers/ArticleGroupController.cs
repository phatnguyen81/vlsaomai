using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Admin;
using Nop.Admin.Controllers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.Articles.Domain;
using Toi.Plugin.Misc.Articles.Models;
using Toi.Plugin.Misc.Articles.Services;

namespace Toi.Plugin.Misc.Articles.Controllers
{
    public class ArticleGroupController : BaseNopController
    {
        #region fields

        private readonly IPermissionService _permissionService;
        private readonly IArticleService _articleService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IStoreService _storeService;
        private readonly IPictureService _pictureService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ICustomerActivityService _customerActivityService;

        #endregion

        #region ctors
        public ArticleGroupController(IPermissionService permissionService,
            IArticleService articleService, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            IStoreMappingService storeMappingService, 
            ILocalizedEntityService localizedEntityService, 
            IStoreService storeService,
            IPictureService pictureService, 
            IUrlRecordService urlRecordService, 
            ICustomerActivityService customerActivityService)
        {
            _permissionService = permissionService;
            _articleService = articleService;
            _languageService = languageService;
            _localizationService = localizationService;
            _storeMappingService = storeMappingService;
            _localizedEntityService = localizedEntityService;
            _storeService = storeService;
            _pictureService = pictureService;
            _urlRecordService = urlRecordService;
            _customerActivityService = customerActivityService;
        }

        #endregion

        #region Utilities
        [NonAction]
        private void PrepareStoresMappingModel(ArticleGroupModel model, ArticleGroup articleGroup, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableStores = _storeService
                .GetAllStores()
                .Select(s => s.ToModel())
                .ToList();
            if (!excludeProperties)
            {
                if (articleGroup != null)
                {
                    model.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(articleGroup);
                }
                else
                {
                    model.SelectedStoreIds = new int[0];
                }
            }
        }
        [NonAction]
        protected void UpdateLocales(ArticleGroup articleGroup, ArticleGroupModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(articleGroup,
                                                               x => x.Name,
                                                               localized.Name,
                                                               localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(articleGroup,
                                                           x => x.Description,
                                                           localized.Description,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(articleGroup,
                                                           x => x.MetaKeywords,
                                                           localized.MetaKeywords,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(articleGroup,
                                                           x => x.MetaDescription,
                                                           localized.MetaDescription,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(articleGroup,
                                                           x => x.MetaTitle,
                                                           localized.MetaTitle,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(articleGroup,
                                                           x => x.SeName,
                                                           localized.SeName,
                                                           localized.LanguageId);
            }
        }

        [NonAction]
        protected void UpdatePictureSeoNames(ArticleGroup articleGroup)
        {
            var picture = _pictureService.GetPictureById(articleGroup.PictureId);
            if (picture != null)
                _pictureService.SetSeoFilename(picture.Id, _pictureService.GetPictureSeName(articleGroup.Name));
        }
        [NonAction]
        protected void SaveStoreMappings(ArticleGroup articleGroup, ArticleGroupModel model)
        {
            var existingStoreMappings = _storeMappingService.GetStoreMappings(articleGroup);
            var allStores = _storeService.GetAllStores();
            foreach (var store in allStores)
            {
                if (model.SelectedStoreIds != null && model.SelectedStoreIds.Contains(store.Id))
                {
                    //new role
                    if (existingStoreMappings.Count(sm => sm.StoreId == store.Id) == 0)
                        _storeMappingService.InsertStoreMapping(articleGroup, store.Id);
                }
                else
                {
                    //removed role
                    var storeMappingToDelete = existingStoreMappings.FirstOrDefault(sm => sm.StoreId == store.Id);
                    if (storeMappingToDelete != null)
                        _storeMappingService.DeleteStoreMapping(storeMappingToDelete);
                }
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
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            //    return AccessDeniedView();
            //var wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
            //var abc = RouteTable.Routes.GetRouteData(wrapper);
            var model = new ArticleGroupListModel();
            return View(model);
        }

        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command, ArticleGroupListModel model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            //    return AccessDeniedView();

            var articleGroups = _articleService.GetAllArticleGroups(model.SearchArticleGroupName,
                command.Page - 1, command.PageSize, true);
            var gridModel = new GridModel<ArticleGroupModel>
            {
                Data = articleGroups.Select(x =>
                {
                    var categoryModel = x.ToModel();
                    categoryModel.Breadcrumb = x.GetFormattedBreadCrumb(_articleService);
                    return categoryModel;
                }),
                Total = articleGroups.TotalCount
            };
            return new JsonResult
            {
                Data = gridModel
            };
        }

        //ajax
        public ActionResult AllArticleGroups(string text, int selectedId)
        {
            var articleGroups = _articleService.GetAllArticleGroups(showHidden:true);
            articleGroups.Insert(0, new ArticleGroup { Name = "[None]", Id = 0 });
            var selectList = new List<SelectListItem>();
            foreach (var c in articleGroups)
                selectList.Add(new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.GetFormattedBreadCrumb(_articleService),
                    Selected = c.Id == selectedId
                });

            //var selectList = new SelectList(categories, "Id", "Name", selectedId);
            return new JsonResult { Data = selectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region Create / Edit / Delete

        public ActionResult Create()
        {

            var model = new ArticleGroupModel();
            //parent categories
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            //locales
            AddLocales(_languageService, model.Locales);
            //Stores
            PrepareStoresMappingModel(model, null, false);
            model.Published = true;

            return View(model);

        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ArticleGroupModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var articleGroup = model.ToEntity();
                articleGroup.CreatedOnUtc = DateTime.UtcNow;
                articleGroup.UpdatedOnUtc = DateTime.UtcNow;
                _articleService.InsertArticleGroup(articleGroup);
                //search engine name
                model.SeName = articleGroup.ValidateSeName(model.SeName, articleGroup.Name, true);
                _urlRecordService.SaveSlug(articleGroup, model.SeName, 0);
                //locales
                UpdateLocales(articleGroup, model);
                _articleService.UpdateArticleGroup(articleGroup);
                //update picture seo file name
                UpdatePictureSeoNames(articleGroup);
                //Stores
                SaveStoreMappings(articleGroup, model);

                //activity log
                _customerActivityService.InsertActivity("AddNewArticleGroup", _localizationService.GetResource("ActivityLog.AddNewArticleGroup"), articleGroup.Name);

                SuccessNotification(_localizationService.GetResource("Admin.Plugins.ArticleManagement.ArticleGroups.Added"));
                return continueEditing
                    ? RedirectToAction("Edit", new { id = articleGroup.Id })
                    : RedirectToAction("List");
            }

            //parent categories
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.ParentGroupId > 0)
            {
                var parentArticleGroup = _articleService.GetArticleGroupById(model.ParentGroupId);
                if (parentArticleGroup != null && !parentArticleGroup.Deleted)
                    model.ParentGroups.Add(new DropDownItem { Text = parentArticleGroup.GetFormattedBreadCrumb(_articleService), Value = parentArticleGroup.Id.ToString() });
                else
                    model.ParentGroupId = 0;
            }
            //Stores
            PrepareStoresMappingModel(model, null, true);
            return View(model);

        }

        public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var articleGroup = _articleService.GetArticleGroupById(id);
            if (articleGroup == null || articleGroup.Deleted)
                //No category found with the specified id
                return RedirectToAction("List");

            var model = articleGroup.ToModel();
            //parent categories
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.ParentGroupId > 0)
            {
                var parentCategory = _articleService.GetArticleGroupById(model.ParentGroupId);
                if (parentCategory != null && !parentCategory.Deleted)
                    model.ParentGroups.Add(new DropDownItem { Text = parentCategory.GetFormattedBreadCrumb(_articleService), Value = parentCategory.Id.ToString() });
                else
                    model.ParentGroupId = 0;
            }
            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Name = articleGroup.GetLocalized(x => x.Name, languageId, false, false);
                locale.Description = articleGroup.GetLocalized(x => x.Description, languageId, false, false);
                locale.MetaKeywords = articleGroup.GetLocalized(x => x.MetaKeywords, languageId, false, false);
                locale.MetaDescription = articleGroup.GetLocalized(x => x.MetaDescription, languageId, false, false);
                locale.MetaTitle = articleGroup.GetLocalized(x => x.MetaTitle, languageId, false, false);
                locale.SeName = articleGroup.GetSeName(languageId, false, false);
            });
            //Stores
            PrepareStoresMappingModel(model, articleGroup, false);

            return View(model);

        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(ArticleGroupModel model, bool continueEditing)
        {
          
            var articleGroup = _articleService.GetArticleGroupById(model.Id);
            if (articleGroup == null || articleGroup.Deleted)
                //No category found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                int prevPictureId = articleGroup.PictureId;
                articleGroup = model.ToEntity(articleGroup);
                articleGroup.UpdatedOnUtc = DateTime.UtcNow;
                _articleService.UpdateArticleGroup(articleGroup);
                //search engine name
                model.SeName = articleGroup.ValidateSeName(model.SeName, articleGroup.Name, true);
                _urlRecordService.SaveSlug(articleGroup, model.SeName, 0);
                //locales
                UpdateLocales(articleGroup, model);
                _articleService.UpdateArticleGroup(articleGroup);
                //delete an old picture (if deleted or updated)
                if (prevPictureId > 0 && prevPictureId != articleGroup.PictureId)
                {
                    var prevPicture = _pictureService.GetPictureById(prevPictureId);
                    if (prevPicture != null)
                        _pictureService.DeletePicture(prevPicture);
                }
                //update picture seo file name
                UpdatePictureSeoNames(articleGroup);
                //Stores
                SaveStoreMappings(articleGroup, model);

                //activity log
                _customerActivityService.InsertActivity("EditArticleGroup", _localizationService.GetResource("ActivityLog.EditArticleGroup"), articleGroup.Name);

                SuccessNotification(_localizationService.GetResource("Admin.Plugins.ArticleManagement.ArticleGroups.Updated"));
                return continueEditing
                    ? RedirectToAction("Edit", new{ id = articleGroup.Id })
                    : RedirectToAction("List" );
            }


            //If we got this far, something failed, redisplay form
            //parent categories
            model.ParentGroups = new List<DropDownItem> { new DropDownItem { Text = "[None]", Value = "0" } };
            if (model.ParentGroupId > 0)
            {
                var parentCategory = _articleService.GetArticleGroupById(model.ParentGroupId);
                if (parentCategory != null && !parentCategory.Deleted)
                    model.ParentGroups.Add(new DropDownItem { Text = parentCategory.GetFormattedBreadCrumb(_articleService), Value = parentCategory.Id.ToString() });
                else
                    model.ParentGroupId = 0;
            }
            //Stores
            PrepareStoresMappingModel(model, articleGroup, true);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var articleGroup = _articleService.GetArticleGroupById(id);
            if (articleGroup == null)
                //No articleGroup found with the specified id
                return RedirectToAction("List");

            _articleService.DeleteArticleGroup(articleGroup);

            //activity log
            _customerActivityService.InsertActivity("DeleteArticleGroup", _localizationService.GetResource("ActivityLog.DeleteArticleGroup"), articleGroup.Name);

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.ArticleManagement.ArticleGroups.Deleted"));
            return RedirectToAction("List");
        }


        #endregion
    }
}
