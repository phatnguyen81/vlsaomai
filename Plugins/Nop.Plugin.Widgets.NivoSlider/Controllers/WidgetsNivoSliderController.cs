﻿using System.Web.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.NivoSlider.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.NivoSlider.Controllers
{
    public class WidgetsNivoSliderController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;

        public WidgetsNivoSliderController(IWorkContext workContext,
            IStoreContext storeContext, IStoreService storeService, 
            IPictureService pictureService, ISettingService settingService)
        {
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._storeService = storeService;
            this._pictureService = pictureService;
            this._settingService = settingService;
        }
        
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var nivoSliderSettings = _settingService.LoadSetting<NivoSliderSettings>(storeScope);
            var model = new ConfigurationModel();
            model.Picture1Id = nivoSliderSettings.Picture1Id;
            model.Text1 = nivoSliderSettings.Text1;
            model.Link1 = nivoSliderSettings.Link1;
            model.Picture2Id = nivoSliderSettings.Picture2Id;
            model.Text2 = nivoSliderSettings.Text2;
            model.Link2 = nivoSliderSettings.Link2;
            model.Picture3Id = nivoSliderSettings.Picture3Id;
            model.Text3 = nivoSliderSettings.Text3;
            model.Link3 = nivoSliderSettings.Link3;
            model.Picture4Id = nivoSliderSettings.Picture4Id;
            model.Text4 = nivoSliderSettings.Text4;
            model.Link4 = nivoSliderSettings.Link4;
            model.Picture5Id = nivoSliderSettings.Picture5Id;
            model.Text5 = nivoSliderSettings.Text5;
            model.Link5 = nivoSliderSettings.Link5;
            model.Picture6Id = nivoSliderSettings.Picture6Id;
            model.Text6 = nivoSliderSettings.Text6;
            model.Link6 = nivoSliderSettings.Link6;
            model.Picture7Id = nivoSliderSettings.Picture7Id;
            model.Text7 = nivoSliderSettings.Text7;
            model.Link7 = nivoSliderSettings.Link7;
            model.Picture8Id = nivoSliderSettings.Picture8Id;
            model.Text8 = nivoSliderSettings.Text8;
            model.Link8 = nivoSliderSettings.Link8;
            model.Picture9Id = nivoSliderSettings.Picture9Id;
            model.Text9 = nivoSliderSettings.Text9;
            model.Link9 = nivoSliderSettings.Link9;
            model.Picture10Id = nivoSliderSettings.Picture10Id;
            model.Text10 = nivoSliderSettings.Text10;
            model.Link10 = nivoSliderSettings.Link10;
            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {
                model.Picture1Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture1Id, storeScope);
                model.Text1_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text1, storeScope);
                model.Link1_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link1, storeScope);
                model.Picture2Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture2Id, storeScope);
                model.Text2_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text2, storeScope);
                model.Link2_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link2, storeScope);
                model.Picture3Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture3Id, storeScope);
                model.Text3_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text3, storeScope);
                model.Link3_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link3, storeScope);
                model.Picture4Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture4Id, storeScope);
                model.Text4_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text4, storeScope);
                model.Link4_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link4, storeScope);
                model.Picture5Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture5Id, storeScope);
                model.Text5_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text5, storeScope);
                model.Link5_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link5, storeScope);
                model.Picture6Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture6Id, storeScope);
                model.Text6_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text6, storeScope);
                model.Link6_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link6, storeScope);
                model.Picture7Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture7Id, storeScope);
                model.Text7_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text7, storeScope);
                model.Link7_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link7, storeScope);
                model.Picture8Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture8Id, storeScope);
                model.Text8_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text8, storeScope);
                model.Link8_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link8, storeScope);
                model.Picture9Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture9Id, storeScope);
                model.Text9_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text9, storeScope);
                model.Link9_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link9, storeScope);
                model.Picture10Id_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Picture10Id, storeScope);
                model.Text10_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Text10, storeScope);
                model.Link10_OverrideForStore = _settingService.SettingExists(nivoSliderSettings, x => x.Link10, storeScope);
            }

            return View("Nop.Plugin.Widgets.NivoSlider.Views.WidgetsNivoSlider.Configure", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var nivoSliderSettings = _settingService.LoadSetting<NivoSliderSettings>(storeScope);
            nivoSliderSettings.Picture1Id = model.Picture1Id;
            nivoSliderSettings.Text1 = model.Text1;
            nivoSliderSettings.Link1 = model.Link1;
            nivoSliderSettings.Picture2Id = model.Picture2Id;
            nivoSliderSettings.Text2 = model.Text2;
            nivoSliderSettings.Link2 = model.Link2;
            nivoSliderSettings.Picture3Id = model.Picture3Id;
            nivoSliderSettings.Text3 = model.Text3;
            nivoSliderSettings.Link3 = model.Link3;
            nivoSliderSettings.Picture4Id = model.Picture4Id;
            nivoSliderSettings.Text4 = model.Text4;
            nivoSliderSettings.Link4 = model.Link4;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            if (model.Picture1Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Picture1Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Picture1Id, storeScope);
            
            if (model.Text1_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Text1, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Text1, storeScope);
            
            if (model.Link1_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Link1, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Link1, storeScope);
            
            if (model.Picture2Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Picture2Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Picture2Id, storeScope);
            
            if (model.Text2_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Text2, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Text2, storeScope);
            
            if (model.Link2_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Link2, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Link2, storeScope);
            
            if (model.Picture3Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Picture3Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Picture3Id, storeScope);
            
            if (model.Text3_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Text3, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Text3, storeScope);
            
            if (model.Link3_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Link3, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Link3, storeScope);
            
            if (model.Picture4Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Picture4Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Picture4Id, storeScope);
            
            if (model.Text4_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Text4, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Text4, storeScope);

            if (model.Link4_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Link4, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Link4, storeScope);

            nivoSliderSettings.Picture5Id = model.Picture5Id;
            nivoSliderSettings.Text5 = model.Text5;
            nivoSliderSettings.Link5 = model.Link5;

            if (model.Picture5Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Picture5Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Picture5Id, storeScope);

            if (model.Text5_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Text5, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Text5, storeScope);

            if (model.Link5_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Link5, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Link5, storeScope);

            nivoSliderSettings.Picture6Id = model.Picture6Id;
            nivoSliderSettings.Text6 = model.Text6;
            nivoSliderSettings.Link6 = model.Link6;

            if (model.Picture6Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Picture6Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Picture6Id, storeScope);

            if (model.Text6_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Text6, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Text6, storeScope);

            if (model.Link6_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Link6, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Link6, storeScope);

            nivoSliderSettings.Picture7Id = model.Picture7Id;
            nivoSliderSettings.Text7 = model.Text7;
            nivoSliderSettings.Link7 = model.Link7;

            if (model.Picture7Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Picture7Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Picture7Id, storeScope);

            if (model.Text7_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Text7, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Text7, storeScope);

            if (model.Link7_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Link7, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Link7, storeScope);

            nivoSliderSettings.Picture8Id = model.Picture8Id;
            nivoSliderSettings.Text8 = model.Text8;
            nivoSliderSettings.Link8 = model.Link8;

            if (model.Picture8Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Picture8Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Picture8Id, storeScope);

            if (model.Text8_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Text8, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Text8, storeScope);

            if (model.Link8_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Link8, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Link8, storeScope);

            nivoSliderSettings.Picture9Id = model.Picture9Id;
            nivoSliderSettings.Text9 = model.Text9;
            nivoSliderSettings.Link9 = model.Link9;

            if (model.Picture9Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Picture9Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Picture9Id, storeScope);

            if (model.Text9_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Text9, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Text9, storeScope);

            if (model.Link9_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Link9, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Link9, storeScope);

            nivoSliderSettings.Picture10Id = model.Picture10Id;
            nivoSliderSettings.Text10 = model.Text10;
            nivoSliderSettings.Link10 = model.Link10;

            if (model.Picture10Id_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Picture10Id, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Picture10Id, storeScope);

            if (model.Text10_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Text10, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Text10, storeScope);

            if (model.Link10_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(nivoSliderSettings, x => x.Link10, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(nivoSliderSettings, x => x.Link10, storeScope);

            //now clear settings cache
            _settingService.ClearCache();
            
            return Configure();
        }

        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone)
        {
            var nivoSliderSettings = _settingService.LoadSetting<NivoSliderSettings>(_storeContext.CurrentStore.Id);

            var model = new PublicInfoModel();
            model.Picture1Url = _pictureService.GetPictureUrl(nivoSliderSettings.Picture1Id, showDefaultPicture: false);
            model.Text1 = nivoSliderSettings.Text1;
            model.Link1 = nivoSliderSettings.Link1;

            model.Picture2Url = _pictureService.GetPictureUrl(nivoSliderSettings.Picture2Id, showDefaultPicture: false);
            model.Text2 = nivoSliderSettings.Text2;
            model.Link2 = nivoSliderSettings.Link2;

            model.Picture3Url = _pictureService.GetPictureUrl(nivoSliderSettings.Picture3Id, showDefaultPicture: false);
            model.Text3 = nivoSliderSettings.Text3;
            model.Link3 = nivoSliderSettings.Link3;

            model.Picture4Url = _pictureService.GetPictureUrl(nivoSliderSettings.Picture4Id, showDefaultPicture: false);
            model.Text4 = nivoSliderSettings.Text4;
            model.Link4 = nivoSliderSettings.Link4;

            model.Picture5Url = _pictureService.GetPictureUrl(nivoSliderSettings.Picture5Id, showDefaultPicture: false);
            model.Text5 = nivoSliderSettings.Text5;
            model.Link5 = nivoSliderSettings.Link5;

            model.Picture6Url = _pictureService.GetPictureUrl(nivoSliderSettings.Picture6Id, showDefaultPicture: false);
            model.Text6 = nivoSliderSettings.Text6;
            model.Link6 = nivoSliderSettings.Link6;

            model.Picture7Url = _pictureService.GetPictureUrl(nivoSliderSettings.Picture7Id, showDefaultPicture: false);
            model.Text7 = nivoSliderSettings.Text7;
            model.Link7 = nivoSliderSettings.Link7;

            model.Picture8Url = _pictureService.GetPictureUrl(nivoSliderSettings.Picture8Id, showDefaultPicture: false);
            model.Text8 = nivoSliderSettings.Text8;
            model.Link8 = nivoSliderSettings.Link8;

            model.Picture9Url = _pictureService.GetPictureUrl(nivoSliderSettings.Picture9Id, showDefaultPicture: false);
            model.Text9 = nivoSliderSettings.Text9;
            model.Link9 = nivoSliderSettings.Link9;

            model.Picture10Url = _pictureService.GetPictureUrl(nivoSliderSettings.Picture10Id, showDefaultPicture: false);
            model.Text10 = nivoSliderSettings.Text10;
            model.Link10 = nivoSliderSettings.Link10;

            if (string.IsNullOrEmpty(model.Picture1Url) && string.IsNullOrEmpty(model.Picture2Url) &&
                string.IsNullOrEmpty(model.Picture3Url) && string.IsNullOrEmpty(model.Picture4Url) &&
                string.IsNullOrEmpty(model.Picture5Url) && string.IsNullOrEmpty(model.Picture6Url) &&
                string.IsNullOrEmpty(model.Picture7Url) && string.IsNullOrEmpty(model.Picture8Url) &&
                string.IsNullOrEmpty(model.Picture9Url) && string.IsNullOrEmpty(model.Picture10Url) 
                )
                //no pictures uploaded
                return Content("");
            

            return View("Nop.Plugin.Widgets.NivoSlider.Views.WidgetsNivoSlider.PublicInfo", model);
        }
    }
}