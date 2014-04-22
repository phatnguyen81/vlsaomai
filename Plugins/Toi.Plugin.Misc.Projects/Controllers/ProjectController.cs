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
using Toi.Plugin.Misc.Projects.Domain;
using Toi.Plugin.Misc.Projects.Models;
using Toi.Plugin.Misc.Projects.Services;

namespace Toi.Plugin.Misc.Projects.Controllers
{
    public class ProjectController : BaseNopController
    {
         #region fields

        private readonly IPermissionService _permissionService;
        private readonly IProjectService _projectService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        #region ctors
        public ProjectController(IPermissionService permissionService,
            IProjectService projectService, 
            ILanguageService languageService, 
            ILocalizationService localizationService, 
            ILocalizedEntityService localizedEntityService, 
            IPictureService pictureService, 
            IUrlRecordService urlRecordService, 
            ICustomerActivityService customerActivityService, IDateTimeHelper dateTimeHelper)
        {
            _permissionService = permissionService;
            _projectService = projectService;
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
        protected void UpdateLocales(Project project, ProjectModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(project,
                                                               x => x.Title,
                                                               localized.Title,
                                                               localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(project,
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
            var model = new ProjectListModel();
            
            return View(model);
        }
        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command, ProjectListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var projects = _projectService.GetAllProjects(model.SearchProjectName,pageIndex: command.Page - 1,pageSize: command.PageSize);
            var gridModel = new GridModel<ProjectModel>
            {
                Data = projects.Select(x =>
                {
                    var m = x.ToModel();
                    m.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc);
                    return m;
                }),
                Total = projects.TotalCount
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
            var model = new ProjectModel();
            AddLocales(_languageService, model.Locales);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ProjectModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var project = model.ToEntity();
                project.CreatedOnUtc = DateTime.UtcNow;
                _projectService.InsertProject(project);

                //locales
                UpdateLocales(project, model);
                _projectService.UpdateProject(project);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Projects.Project.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = project.Id }) : RedirectToAction("List");
            }
           
            return View(model);
        }
        public ActionResult Edit(int id)
        {

            var project = _projectService.GetProjectById(id);
            if (project == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            var model = project.ToModel();

            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Title = project.GetLocalized(x => x.Title, languageId, false, false);
                locale.Full = project.GetLocalized(x => x.Full, languageId, false, false);
            });
           
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(ProjectModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageNews))
                return AccessDeniedView();

            var project = _projectService.GetProjectById(model.Id);
            if (project == null)
                //No news item found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                project = model.ToEntity(project);
                _projectService.UpdateProject(project);


                //locales
                UpdateLocales(project, model);
                _projectService.UpdateProject(project);

                SuccessNotification(_localizationService.GetResource("Toi.Plugin.Misc.Projects.Project.Updated"));
                return continueEditing ? RedirectToAction("Edit", new { id = project.Id }) : RedirectToAction("List");
            }
            return View(model);
        }
        #endregion

      
    }
}
