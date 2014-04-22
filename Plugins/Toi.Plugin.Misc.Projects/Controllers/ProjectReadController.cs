using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Admin.Controllers;
using Nop.Core.Infrastructure;
using Nop.Services.Media;
using Nop.Web.Framework.UI.Paging;
using Toi.Plugin.Misc.Projects.Models;
using Toi.Plugin.Misc.Projects.Services;

namespace Toi.Plugin.Misc.Projects.Controllers
{
    public class ProjectReadController : BaseNopController
    {
        #region Fields
        private readonly IProjectService _projectService;
        private readonly IPictureService _pictureService;
        #endregion

        #region Ctors
        public ProjectReadController(IProjectService projectService,
            IPictureService pictureService)
        {
            _projectService = projectService;
            _pictureService = pictureService;
        }

        #endregion


        public ActionResult Index(ProjectsPagingFilteringModel command)
        {
            if (command.PageNumber <= 0) command.PageNumber = 1;
            var model = new ProjectIndexModel();
            var projects = _projectService.GetAllProjects(pageIndex: command.PageNumber - 1, pageSize: 5);
            model.Projects = projects.Select(q =>
            {
                var project = q.ToModel();
                project.PictureModel = new PictureModel
                {
                    ImageUrl = _pictureService.GetPictureUrl(q.PictureId, 300),
                    FullSizeImageUrl = _pictureService.GetPictureUrl(q.PictureId)
                };
                return project;
            }).ToList();
            model.PagingFilteringContext.LoadPagedList(projects);
            return View(model);

        }
        public static class ControllerUtilities
        {
            public static IController ResolveNopWebControllerByName(string typeFullName)
            {
                // Since we're not referencing Nop.Web, we must look through the loaded assemblies to find the controller type by name. 
                // Since we know what the type's full name is, we can do a search for it by fullname
                // then resolve its instance from the returned type.
                var assemblies = EngineContext.Current.Resolve<ITypeFinder>().GetAssemblies();

                var controllerType = (from assemply in assemblies
                                      from type in assemply.GetTypes()
                                      where type.FullName.Equals(typeFullName, StringComparison.InvariantCultureIgnoreCase)
                                      select type).FirstOrDefault();

                if (controllerType == null) // Default fallback if the type cannot be found.
                    return null;

                IController controller = EngineContext.Current.Resolve(controllerType) as IController;

                return controller;
            }
        }
        protected virtual ActionResult InvokeHttp404()
        {
            var commonController = ControllerUtilities.ResolveNopWebControllerByName("Nop.Web.Controllers.CommonController");

            if (commonController == null) // Default fallback if the type cannot be found.
                return RedirectToAction("PageNotFound", "Common");

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Common");
            routeData.Values.Add("action", "PageNotFound");

            commonController.Execute(new RequestContext(this.HttpContext, routeData));

            return new EmptyResult();
        }
    }
}
