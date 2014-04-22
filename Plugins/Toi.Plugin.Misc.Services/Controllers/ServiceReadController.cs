using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Admin.Controllers;
using Nop.Core.Infrastructure;
using Nop.Services.Media;
using Toi.Plugin.Misc.Services.Models;
using Toi.Plugin.Misc.Services.Services;

namespace Toi.Plugin.Misc.Services.Controllers
{
    public class ServiceReadController : BaseNopController
    {
        #region Fields
        private readonly IServiceService _serviceService;
        private readonly IPictureService _pictureService;
        #endregion

        #region Ctors
        public ServiceReadController(IServiceService serviceService,
            IPictureService pictureService)
        {
            _serviceService = serviceService;
            _pictureService = pictureService;
        }

        #endregion


        public ActionResult Index(ServicesPagingFilteringModel command)
        {
            if (command.PageNumber <= 0) command.PageNumber = 1;
            var model = new ServiceIndexModel();
            var services = _serviceService.GetAllServices(pageIndex: command.PageNumber - 1, pageSize: 5);
            model.Services = services.Select(q =>
            {
                var service = q.ToModel();
                service.PictureModel = new PictureModel
                {
                    ImageUrl = _pictureService.GetPictureUrl(q.PictureId, 300),
                    FullSizeImageUrl = _pictureService.GetPictureUrl(q.PictureId)
                };
                return service;
            }).ToList();
            model.PagingFilteringContext.LoadPagedList(services);
            return View(model);

        }

        public ActionResult Detail(int id)
        {

            var evt = _serviceService.GetServiceById(id);
            if (evt == null) return RedirectToAction("Index");
            var model = evt.ToModel();
            model.PictureModel.ImageUrl = _pictureService.GetPictureUrl(evt.PictureId, 290);
            model.PictureModel.FullSizeImageUrl = _pictureService.GetPictureUrl(evt.PictureId);

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
