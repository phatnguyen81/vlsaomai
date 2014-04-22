using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Admin.Controllers;
using Nop.Core.Infrastructure;
using Nop.Services.Media;
using Toi.Plugin.Misc.NewsExs.Services;
using Toi.Plugin.Misc.NewsExs.Models;

namespace Toi.Plugin.Misc.NewsExs.Controllers
{
    public class NewsExReadController : BaseNopController
    {
        #region Fields
        private readonly INewsExService _newsExService;
        private readonly IPictureService _pictureService;
        #endregion

        #region Ctors
        public NewsExReadController(INewsExService newsExService,
            IPictureService pictureService)
        {
            _newsExService = newsExService;
            _pictureService = pictureService;
        }

        #endregion


        public ActionResult Index(NewsExsPagingFilteringModel command)
        {
            if (command.PageNumber <= 0) command.PageNumber = 1;
            var model = new NewsExIndexModel();
            var newsExs = _newsExService.GetAllNewsExs(pageIndex: command.PageNumber - 1, pageSize: 10);
            model.NewsExs = newsExs.Select(q =>
            {
                var project = q.ToModel();
                project.PictureModel = new PictureModel
                {
                    ImageUrl = _pictureService.GetPictureUrl(q.PictureId, 290),
                    FullSizeImageUrl = _pictureService.GetPictureUrl(q.PictureId)
                };
                return project;
            }).ToList();
            model.PagingFilteringContext.LoadPagedList(newsExs);
            return View(model);

        }

        public ActionResult Detail(int id)
        {
            
            var newsEx = _newsExService.GetNewsExById(id);
            if (newsEx == null) return RedirectToAction("Index");
            var model = newsEx.ToModel();
            model.PictureModel.ImageUrl = _pictureService.GetPictureUrl(newsEx.PictureId, 290);
            model.PictureModel.FullSizeImageUrl = _pictureService.GetPictureUrl(newsEx.PictureId);
          
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
