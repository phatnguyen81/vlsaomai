using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Admin.Controllers;
using Nop.Core.Infrastructure;
using Nop.Services.Media;
using Toi.Plugin.Misc.Branches.Models;
using Toi.Plugin.Misc.Branches.Services;

namespace Toi.Plugin.Misc.Branches.Controllers
{
    public class BranchReadController : BaseNopController
    {
        #region Fields
        private readonly IBranchService _branchService;
        private readonly IPictureService _pictureService;
        private readonly BranchSettings _branchSettings;
        #endregion

        #region Ctors
        public BranchReadController(IBranchService branchService,
            IPictureService pictureService,
            BranchSettings branchSettings)
        {
            _branchService = branchService;
            _pictureService = pictureService;
            _branchSettings = branchSettings;
        }

        #endregion

        public ActionResult Detail(int id)
        {
            var branch = _branchService.GetBranchById(id);
            if (branch == null)
                return InvokeHttp404();
            var model = branch.ToModel();
            model.PictureModel = new PictureModel
            {
                FullSizeImageUrl = _pictureService.GetPictureUrl(model.PictureId),
                ImageUrl = _pictureService.GetPictureUrl(model.PictureId, _branchSettings.BranchThumbPictureSize)
            };
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
            var commonController = BranchGroupReadController.ControllerUtilities.ResolveNopWebControllerByName("Nop.Web.Controllers.CommonController");

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
