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
using Toi.Plugin.Misc.Events.Domain;
using Toi.Plugin.Misc.Events.Models;
using Toi.Plugin.Misc.Events.Services;

namespace Toi.Plugin.Misc.Events.Controllers
{
    public class EventController : BaseNopController
    {
         #region fields

        private readonly IPermissionService _permissionService;
        private readonly IEventService _eventService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        #region ctors
        public EventController(IPermissionService permissionService,
            IEventService eventService, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            ILocalizedEntityService localizedEntityService, 
            IPictureService pictureService, 
            IUrlRecordService urlRecordService, 
            ICustomerActivityService customerActivityService, IDateTimeHelper dateTimeHelper)
        {
            _permissionService = permissionService;
            _eventService = eventService;
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
        protected void UpdateLocales(Event @event, EventModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(@event,
                                                               x => x.Title,
                                                               localized.Title,
                                                               localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(@event,
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
            var model = new EventListModel();
            
            return View(model);
        }
        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command, EventListModel model)
        {

            var evts = _eventService.GetAllEvents(model.SearchEventName,pageIndex: command.Page - 1,pageSize: command.PageSize);
            var gridModel = new GridModel<EventModel>
            {
                Data = evts.Select(x =>
                {
                    var m = x.ToModel();
                    m.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc);
                    return m;
                }),
                Total = evts.TotalCount
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
            var model = new EventModel();
            AddLocales(_languageService, model.Locales);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(EventModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var evt = model.ToEntity();
                evt.CreatedOnUtc = DateTime.UtcNow;
                _eventService.InsertEvent(evt);

                //locales
                UpdateLocales(evt, model);
                _eventService.UpdateEvent(evt);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Events.Event.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = evt.Id }) : RedirectToAction("List");
            }
           
            return View(model);
        }
        public ActionResult Edit(int id)
        {

            var evt = _eventService.GetEventById(id);
            if (evt == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            var model = evt.ToModel();

            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Title = evt.GetLocalized(x => x.Title, languageId, false, false);
                locale.Full = evt.GetLocalized(x => x.Full, languageId, false, false);
            });
           
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(EventModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var evt = _eventService.GetEventById(model.Id);
            if (evt == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                evt = model.ToEntity(evt);
                _eventService.UpdateEvent(evt);


                //locales
                UpdateLocales(evt, model);
                _eventService.UpdateEvent(evt);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Events.Event.Updated"));
                return continueEditing ? RedirectToAction("Edit", new { id = evt.Id }) : RedirectToAction("List");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var evt = _eventService.GetEventById(id);
            if (evt == null)
                //No branchGroup found with the specified id
                return RedirectToAction("List");

            _eventService.DeleteEvent(evt);

            //activity log
            _customerActivityService.InsertActivity("DeleteEvent", _localizationService.GetResource("ActivityLog.DeleteEvent"), evt.Title);

            SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Events.Event.Deleted"));
            return RedirectToAction("List");
        }
        #endregion

      
    }
}
