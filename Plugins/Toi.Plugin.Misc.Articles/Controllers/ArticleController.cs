using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Admin.Controllers;
using Nop.Admin.Models.News;
using Nop.Core;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Telerik.Web.Mvc;
using Toi.Plugin.Misc.Articles.Domain;
using Toi.Plugin.Misc.Articles.Models;
using Toi.Plugin.Misc.Articles.Services;

namespace Toi.Plugin.Misc.Articles.Controllers
{
    public class ArticleController : BaseNopController
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
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        #region ctors
        public ArticleController(IPermissionService permissionService,
            IArticleService articleService, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            IStoreMappingService storeMappingService, 
            ILocalizedEntityService localizedEntityService, 
            IStoreService storeService,
            IPictureService pictureService, 
            IUrlRecordService urlRecordService, 
            ICustomerActivityService customerActivityService, IDateTimeHelper dateTimeHelper)
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
            _dateTimeHelper = dateTimeHelper;
        }

        #endregion

        #region Utilitis
        [NonAction]
        protected void UpdateLocales(Article article, ArticleModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(article,
                                                               x => x.Title,
                                                               localized.Title,
                                                               localized.LanguageId);


                _localizedEntityService.SaveLocalizedValue(article,
                                                          x => x.Short,
                                                          localized.MetaKeywords,
                                                          localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(article,
                                                          x => x.Full,
                                                          localized.MetaKeywords,
                                                          localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(article,
                                                           x => x.MetaKeywords,
                                                           localized.MetaKeywords,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(article,
                                                           x => x.MetaDescription,
                                                           localized.MetaDescription,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(article,
                                                           x => x.MetaTitle,
                                                           localized.MetaTitle,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(article,
                                                           x => x.SeName,
                                                           localized.SeName,
                                                           localized.LanguageId);
            }
        }
        private void PrepareArticleModel(ArticleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("ArticleModel");
            }
            model.NumberOfAvailableGroups = _articleService.GetAllArticleGroups().Count;
        }
        #endregion

        #region List
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ArticleListModel();
            
            return View(model);
        }
        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command, ArticleListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var articles = _articleService.GetAllArticles(model.SearchArticleName, command.Page - 1, command.PageSize, true);
            var gridModel = new GridModel<ArticleModel>
            {
                Data = articles.Select(x =>
                {
                    var m = x.ToModel();
                    if (x.StartDateUtc.HasValue)
                        m.StartDate = _dateTimeHelper.ConvertToUserTime(x.StartDateUtc.Value, DateTimeKind.Utc);
                    if (x.EndDateUtc.HasValue)
                        m.EndDate = _dateTimeHelper.ConvertToUserTime(x.EndDateUtc.Value, DateTimeKind.Utc);
                    m.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc);
                    return m;
                }),
                Total = articles.TotalCount
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
            var model = new ArticleModel();
            AddLocales(_languageService, model.Locales);
            model.Published = true;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ArticleModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var article = model.ToEntity();
                article.StartDateUtc = model.StartDate;
                article.EndDateUtc = model.EndDate;
                article.CreatedOnUtc = DateTime.UtcNow;
                _articleService.InsertArticle(article);

                //search engine name
                model.SeName = article.ValidateSeName(model.SeName, article.Title, true);
                _urlRecordService.SaveSlug(article, model.SeName, 0);

                //locales
                UpdateLocales(article, model);
                _articleService.UpdateArticle(article);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Articles.Article.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = article.Id }) : RedirectToAction("List");
            }

            return View(model);
        }
        public ActionResult Edit(int id)
        {

            var article = _articleService.GetArticleById(id);
            if (article == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            var model = article.ToModel();
            PrepareArticleModel(model);
            model.StartDate = article.StartDateUtc;
            model.EndDate = article.EndDateUtc;

            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Title = article.GetLocalized(x => x.Title, languageId, false, false);
                locale.Short = article.GetLocalized(x => x.Short, languageId, false, false);
                locale.Full = article.GetLocalized(x => x.Full, languageId, false, false);
                locale.MetaKeywords = article.GetLocalized(x => x.MetaKeywords, languageId, false, false);
                locale.MetaDescription = article.GetLocalized(x => x.MetaDescription, languageId, false, false);
                locale.MetaTitle = article.GetLocalized(x => x.MetaTitle, languageId, false, false);
                locale.SeName = article.GetSeName(languageId, false, false);
            });

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormNameAttribute("save-continue", "continueEditing")]
        public ActionResult Edit(ArticleModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var article = _articleService.GetArticleById(model.Id);
            if (article == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                article = model.ToEntity(article);
                article.StartDateUtc = model.StartDate;
                article.EndDateUtc = model.EndDate;
                _articleService.UpdateArticle(article);

                //search engine name
                model.SeName = article.ValidateSeName(model.SeName, article.Title, true);
                _urlRecordService.SaveSlug(article, model.SeName, 0);

                //locales
                UpdateLocales(article, model);
                _articleService.UpdateArticle(article);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Articles.Article.Updated"));
                return continueEditing ? RedirectToAction("Edit", new { id = article.Id }) : RedirectToAction("List");
            }
            PrepareArticleModel(model);
            return View(model);
        }
        #endregion

        #region Ajax
        [GridAction(EnableCustomBinding=true)]
        [HttpPost]
        public ActionResult ArticleGroupList(GridCommand command, int articleId)
        {
            var articleToGroups = _articleService.GetArticleToGroupsByArticleId(articleId, showHidden:true);
            var articleGroupModel = articleToGroups
                .Select(x =>
                {
                    return new ArticleModel.ArticleGroupModel()
                    {
                        Id = x.Id,
                        ArticleGroup = _articleService.GetArticleGroupById(x.ArticleGroupId).GetFormattedBreadCrumb(_articleService),
                        ArticleId = x.ArticleId,
                        ArticleGroupId = x.ArticleGroupId,
                        IsHotArticle = x.IsHotArticle,
                        DisplayOrder = x.DisplayOrder
                    };
                })
                .ToList();

            var model = new GridModel<ArticleModel.ArticleGroupModel>
            {
                Data = articleGroupModel,
                Total = articleToGroups.TotalCount
            };

            return new JsonResult
            {
                Data = model
            };
        }
        [GridAction(EnableCustomBinding=true)]
        public ActionResult ArticleGroupInsert(GridCommand command, ArticleModel.ArticleGroupModel model)
        {
            int articleId = model.ArticleId;
            int artileGroupId = int.Parse(model.ArticleGroup);
            if (_articleService.GetArticleToGroupsByArticleId(articleId, 0, 2147483647, true).FindArticleToGroup(artileGroupId, articleId) == null)
            {
                var articleToGroup = new ArticleToGroup
                {
                    ArticleId = articleId,
                    ArticleGroupId = artileGroupId,
                    DisplayOrder = model.DisplayOrder
                };
                _articleService.InsertArticleToGroup(articleToGroup);
            }
            return ArticleGroupList(command, articleId);
        }
        [GridAction(EnableCustomBinding=true)]
        public ActionResult ArticleGroupDelete(int id, GridCommand command)
        {
            ArticleToGroup articleToGroup = _articleService.GetArticleToGroupById(id);
            if (articleToGroup == null)
            {
                throw new ArgumentException("articleToGroupById");
            }
            int articleId = articleToGroup.ArticleId;
            _articleService.DeleteArticleToGroup(articleToGroup);
            return this.ArticleGroupList(command, articleId);
        }

        [GridAction(EnableCustomBinding=true)]
        public ActionResult ArticleGroupUpdate(GridCommand command, ArticleModel.ArticleGroupModel model)
        {
            ArticleToGroup articleToGroup = _articleService.GetArticleToGroupById(model.Id);
            if (articleToGroup == null)
            {
                throw new ArgumentException("articleToGroup");
            }
            articleToGroup.ArticleGroupId = int.Parse(model.ArticleGroup);
            articleToGroup.DisplayOrder = model.DisplayOrder;
            _articleService.UpdateArticleToGroup(articleToGroup);
            return this.ArticleGroupList(command, articleToGroup.ArticleId);
        }
        #endregion
    }
}
