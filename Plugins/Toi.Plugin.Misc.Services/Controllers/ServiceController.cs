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
using Toi.Plugin.Misc.Services.Models;
using Toi.Plugin.Misc.Services.Services;
using Toi.Plugin.Misc.Services.Domain;

namespace Toi.Plugin.Misc.Services.Controllers
{
    public class ServiceController : BaseNopController
    {
         #region fields

        private readonly IPermissionService _permissionService;
        private readonly IServiceService _serviceService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        #region ctors
        public ServiceController(IPermissionService permissionService,
            IServiceService ServiceService, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            ILocalizedEntityService localizedEntityService, 
            IPictureService pictureService, 
            IUrlRecordService urlRecordService, 
            ICustomerActivityService customerActivityService, IDateTimeHelper dateTimeHelper)
        {
            _permissionService = permissionService;
            _serviceService = ServiceService;
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
        protected void UpdateLocales(Service Service, ServiceModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(Service,
                                                               x => x.Title,
                                                               localized.Title,
                                                               localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(Service,
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
            var model = new ServiceListModel();
            
            return View(model);
        }
        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command, ServiceListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var Services = _serviceService.GetAllServices(model.SearchServiceName,pageIndex: command.Page - 1,pageSize: command.PageSize);
            var gridModel = new GridModel<ServiceModel>
            {
                Data = Services.Select(x =>
                {
                    var m = x.ToModel();
                    m.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc);
                    return m;
                }),
                Total = Services.TotalCount
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
            var model = new ServiceModel();
            AddLocales(_languageService, model.Locales);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ServiceModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var Service = model.ToEntity();
                Service.CreatedOnUtc = DateTime.UtcNow;
                _serviceService.InsertService(Service);

                //locales
                UpdateLocales(Service, model);
                _serviceService.UpdateService(Service);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Services.Service.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = Service.Id }) : RedirectToAction("List");
            }
           
            return View(model);
        }
        public ActionResult Edit(int id)
        {

            var Service = _serviceService.GetServiceById(id);
            if (Service == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            var model = Service.ToModel();

            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Title = Service.GetLocalized(x => x.Title, languageId, false, false);
                locale.Full = Service.GetLocalized(x => x.Full, languageId, false, false);
            });
           
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(ServiceModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var Service = _serviceService.GetServiceById(model.Id);
            if (Service == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                Service = model.ToEntity(Service);
                _serviceService.UpdateService(Service);


                //locales
                UpdateLocales(Service, model);
                _serviceService.UpdateService(Service);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Services.Service.Updated"));
                return continueEditing ? RedirectToAction("Edit", new { id = Service.Id }) : RedirectToAction("List");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var service = _serviceService.GetServiceById(id);
            if (service == null)
                //No branchGroup found with the specified id
                return RedirectToAction("List");

            _serviceService.DeleteService(service);

            //activity log
            _customerActivityService.InsertActivity("DeleteService", _localizationService.GetResource("ActivityLog.DeleteService"), service.Title);

            SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Services.Service.Deleted"));
            return RedirectToAction("List");
        }
        #endregion

      
    }
}
