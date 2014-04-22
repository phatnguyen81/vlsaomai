using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Toi.Plugin.Misc.Services.Models;
using Toi.Plugin.Misc.Services.Validators;

namespace Toi.Plugin.Misc.Services.Models
{
    public partial class ServiceListModel : BaseNopModel
    {
        [NopResourceDisplayName("Toi.Plugin.Misc.Services.Service.List.SearchServiceName")]
        [AllowHtml]
        public string SearchServiceName { get; set; }

        public IList<ServiceModel> Services { get; set; }
    }

    [Validator(typeof(ServiceValidator))]
    public class ServiceModel : BaseNopEntityModel, ILocalizedModel<ServiceLocalizedModel>
    {
        public ServiceModel()
        {
            Locales = new List<ServiceLocalizedModel>();
        }


        [NopResourceDisplayName("Toi.Plugin.Misc.Services.Service.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }


        [NopResourceDisplayName("Toi.Plugin.Misc.Services.Service.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }

      
        [NopResourceDisplayName("Toi.Plugin.Misc.Services.Service.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Toi.Plugin.Misc.Services.Service.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Services.Service.Fields.Picture")]
        public int DisplayOrder { get; set; }

        public PictureModel PictureModel { get; set; }
        public IList<ServiceLocalizedModel> Locales { get; set; }
    }
    public partial class ServiceLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Services.Service.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }
        [NopResourceDisplayName("Toi.Plugin.Misc.Services.Service.Fields.Short")]
        [AllowHtml]
        public string Full { get; set; }

    
    }
}
