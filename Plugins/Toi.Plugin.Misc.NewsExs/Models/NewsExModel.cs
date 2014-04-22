using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Toi.Plugin.Misc.NewsExs.Models;
using Toi.Plugin.Misc.NewsExs.Validators;

namespace Toi.Plugin.Misc.NewsExs.Models
{
    public partial class NewsExListModel : BaseNopModel
    {
        [NopResourceDisplayName("Toi.Plugin.Misc.NewsExs.NewsEx.List.SearchNewsExName")]
        [AllowHtml]
        public string SearchNewsExName { get; set; }

        public IList<NewsExModel> NewsExs { get; set; }
    }

    [Validator(typeof(NewsExValidator))]
    public class NewsExModel : BaseNopEntityModel, ILocalizedModel<NewsExLocalizedModel>
    {
        public NewsExModel()
        {
            Locales = new List<NewsExLocalizedModel>();
            PictureModel = new PictureModel();
        }


        [NopResourceDisplayName("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Short")]
        public string Short { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }

      
        [NopResourceDisplayName("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Picture")]
        public int DisplayOrder { get; set; }

        public PictureModel PictureModel { get; set; }
        public IList<NewsExLocalizedModel> Locales { get; set; }
    }
    public partial class NewsExLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Short")]
        [AllowHtml]
        public string Short { get; set; }


        [NopResourceDisplayName("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }



    
    }
}
