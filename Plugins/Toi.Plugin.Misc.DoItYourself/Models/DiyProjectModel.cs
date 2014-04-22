using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Core.Domain.Media;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.UI.Paging;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.DoItYourself.Models;
using Toi.Plugin.Misc.DoItYourself.Validators;

namespace Toi.Plugin.Misc.DoItYourself.Models
{
    public partial class DiyProjectListModel : BaseNopModel
    {
        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.List.SearchDiyProjectName")]
        [AllowHtml]
        public string SearchDiyProjectName { get; set; }

        public BasePageableModel PagingFilteringContext { get; set; }

        public IList<DiyProjectModel> DiyProjects { get; set; }
    }

    [Validator(typeof(DiyProjectValidator))]
    public class DiyProjectModel : BaseNopEntityModel, ILocalizedModel<DiyProjectLocalizedModel>
    {
        public DiyProjectModel()
        {
            Locales = new List<DiyProjectLocalizedModel>();
        }


        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Short")]
        [AllowHtml]
        public string Short { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.ShowOnHomePage")]
        public bool ShowOnHomePage { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Picture")]
        public int PictureId { get; set; }

        public IList<DiyProjectLocalizedModel> Locales { get; set; }

        public int NumberOfAvailableGroups { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.DiyGroup")]
        public int DiyGroupId { get; set; }
        public IList<DropDownItem> DiyGroups { get; set; }

        public PictureModel PictureModel { get; set; }
    }

    public class HotestDiyProjectModel : DiyProjectModel
    {
        public List<string> PictureUrls { get; set; }
    }
    public partial class DiyProjectLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }
        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Short")]
        [AllowHtml]
        public string Short { get; set; }
        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }
    }
}
