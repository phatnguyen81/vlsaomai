using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using HtmlAgilityPack;
using Nop.Admin.Controllers;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using Nop.Services.Media;
using Toi.Plugin.Misc.DoItYourself.Models;
using Toi.Plugin.Misc.DoItYourself.Services;

namespace Toi.Plugin.Misc.DoItYourself.Controllers
{
    public class DiyProjectReadController : BaseNopController
    {
        #region Fields
        private readonly IDiyService _diyService;
        private readonly IPictureService _pictureService;
        private readonly DiySettings _diySettings;
        #endregion

        #region Ctors
        public DiyProjectReadController(IDiyService diyService,
            IPictureService pictureService,
            DiySettings diySettings)
        {
            _diyService = diyService;
            _pictureService = pictureService;
            _diySettings = diySettings;
        }

        #endregion

        public ActionResult Detail(int id)
        {
            var project = _diyService.GetDiyProjectById(id);
            if (project == null)
                return InvokeHttp404();
            var model = new DiyProjectModel();
            model.Id = project.Id;
            model.Full = project.GetLocalized(x => x.Full);
            model.Short = project.GetLocalized(x => x.Short);
            model.SeName = project.GetLocalized(x => x.SeName);
            model.MetaDescription = project.GetLocalized(x => x.MetaDescription);
            model.MetaKeywords = project.GetLocalized(x => x.MetaKeywords);
            model.MetaTitle = project.GetLocalized(x => x.MetaTitle);
            model.Title = project.GetLocalized(x => x.Title);
            model.PictureModel = new PictureModel
            {
                FullSizeImageUrl = _pictureService.GetPictureUrl(model.PictureId),
                ImageUrl = _pictureService.GetPictureUrl(model.PictureId, _diySettings.DiyProjectThumbPictureSize)
            };
            return View(model);
        }

        public string ConvertIsoToUtf8(string source)
        {
            var enc = Encoding.GetEncoding("iso-8859-1");
            var isoBytes = enc.GetBytes(source);
            //var utf8Bytes = Encoding.Convert(enc, Encoding.UTF8, isoBytes);
            return Encoding.UTF8.GetString(isoBytes);
        }
        public List<string> FetchLinksFromSource(string htmlSource)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlSource);
            var nodes = doc.DocumentNode.SelectNodes("//img[@src]");

            return nodes.Select(q => Server.HtmlDecode(q.Attributes["src"].Value)).ToList();
            //var links = new List<string>();
            //if (string.IsNullOrWhiteSpace(htmlSource)) return links;
            //const string regexImgSrc = @"<img.*?src=""(?<url>.*?)"".*?>";
            //var matchesImgSrc = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //foreach (Match m in matchesImgSrc)
            //{
            //    string href = m.Groups[1].Value;
            //    links.Add((new Uri(href)).ToString());
            //}
            //return links;
        }
        public ActionResult HostestProject()
        {
            var projectId = _diySettings.HotestProjectId;
            var project = _diyService.GetDiyProjectById(projectId);
            if (project == null)
                project = _diyService.GetAllDiyProjects(showOnHomePage: true).FirstOrDefault();
            var model = new HotestDiyProjectModel();
            if (project == null)
                return PartialView(model);
            
            model.Id = project.Id;
            model.Full = project.GetLocalized(x => x.Full);
            model.Short = project.GetLocalized(x => x.Short);
            model.SeName = project.GetLocalized(x => x.SeName);
            model.MetaDescription = project.GetLocalized(x => x.MetaDescription);
            model.MetaKeywords = project.GetLocalized(x => x.MetaKeywords);
            model.MetaTitle = project.GetLocalized(x => x.MetaTitle);
            model.Title = project.GetLocalized(x => x.Title);
            model.PictureUrls = FetchLinksFromSource(model.Full);
            return PartialView(model);
        }

        public ActionResult HomePageProjects()
        {
            var projects = _diyService.GetAllDiyProjects(showOnHomePage: true, pageIndex: 0, pageSize: 6);
            var model = projects.Select(q =>
            {
                var projectModel = q.ToModel();
                projectModel.PictureModel = new PictureModel
                {
                    FullSizeImageUrl = _pictureService.GetPictureUrl(q.PictureId),
                    ImageUrl = _pictureService.GetPictureUrl(q.PictureId, _diySettings.DiyProjectThumbPictureSize)
                };
                return projectModel;
            });
            return PartialView(model);
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
            var commonController = DiyGroupReadController.ControllerUtilities.ResolveNopWebControllerByName("Nop.Web.Controllers.CommonController");

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
