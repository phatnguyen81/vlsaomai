using System;
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
using Toi.Plugin.Misc.NewsExs.Domain;
using Toi.Plugin.Misc.NewsExs.Models;
using Toi.Plugin.Misc.NewsExs.Services;

namespace Toi.Plugin.Misc.NewsExs.Controllers
{
    public class NewsExController : BaseNopController
    {
         #region fields

        private readonly IPermissionService _permissionService;
        private readonly INewsExService _newsExService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        #region ctors
        public NewsExController(IPermissionService permissionService,
            INewsExService newsExService, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            ILocalizedEntityService localizedEntityService, 
            IPictureService pictureService, 
            IUrlRecordService urlRecordService, 
            ICustomerActivityService customerActivityService, IDateTimeHelper dateTimeHelper)
        {
            _permissionService = permissionService;
            _newsExService = newsExService;
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
        protected void UpdateLocales(NewsEx @newsEx, NewsExModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(@newsEx,
                                                               x => x.Title,
                                                               localized.Title,
                                                               localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(@newsEx,
                                                               x => x.Full,
                                                               localized.Full,
                                                               localized.LanguageId);


              
            }
        }
     
        #endregion

        #region List
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new NewsExListModel();
            
            return View(model);
        }
        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command, NewsExListModel model)
        {

            var newsExs = _newsExService.GetAllNewsExs(model.SearchNewsExName,pageIndex: command.Page - 1,pageSize: command.PageSize);
            var gridModel = new GridModel<NewsExModel>
            {
                Data = newsExs.Select(x =>
                {
                    var m = x.ToModel();
                    m.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc);
                    return m;
                }),
                Total = newsExs.TotalCount
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
            var model = new NewsExModel();
            AddLocales(_languageService, model.Locales);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(NewsExModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var newsEx = model.ToEntity();
                newsEx.CreatedOnUtc = DateTime.UtcNow;
                _newsExService.InsertNewsEx(newsEx);

                //locales
                UpdateLocales(newsEx, model);
                _newsExService.UpdateNewsEx(newsEx);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.NewsExs.NewsEx.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = newsEx.Id }) : RedirectToAction("List");
            }
           
            return View(model);
        }
        public ActionResult Edit(int id)
        {

            var newsEx = _newsExService.GetNewsExById(id);
            if (newsEx == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            var model = newsEx.ToModel();

            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Title = newsEx.GetLocalized(x => x.Title, languageId, false, false);
                locale.Full = newsEx.GetLocalized(x => x.Full, languageId, false, false);
            });
           
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(NewsExModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var newsEx = _newsExService.GetNewsExById(model.Id);
            if (newsEx == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                newsEx = model.ToEntity(newsEx);
                _newsExService.UpdateNewsEx(newsEx);


                //locales
                UpdateLocales(newsEx, model);
                _newsExService.UpdateNewsEx(newsEx);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.NewsExs.NewsEx.Updated"));
                return continueEditing ? RedirectToAction("Edit", new { id = newsEx.Id }) : RedirectToAction("List");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var news = _newsExService.GetNewsExById(id);
            if (news == null)
                //No branchGroup found with the specified id
                return RedirectToAction("List");

            _newsExService.DeleteService(news);

            //activity log
            _customerActivityService.InsertActivity("DeleteNews", _localizationService.GetResource("ActivityLog.DeleteNews"), news.Title);

            SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.NewsExs.NewsEx.Deleted"));
            return RedirectToAction("List");
        }
        #endregion

      
    }
}
