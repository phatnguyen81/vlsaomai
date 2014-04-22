using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using FluentValidation.Attributes;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.DoItYourself.Models;
using Toi.Plugin.Misc.DoItYourself.Validators;

namespace Toi.Plugin.Misc.DoItYourself.Models
{
    public partial class DiyGroupListModel : BaseNopModel
    {
        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.List.SearchDiyGroupName")]
        [AllowHtml]
        public string SearchDiyGroupName { get; set; }
    }

    [Validator(typeof(DiyGroupValidator))]
    public class DiyGroupModel : BaseNopEntityModel, ILocalizedModel<DiyGroupLocalizedModel>
    {
        public DiyGroupModel()
        {
            Locales = new List<DiyGroupLocalizedModel>();
        }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Parent")]
        public int ParentGroupId { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Deleted")]
        public bool Deleted { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        public IList<DiyGroupLocalizedModel> Locales { get; set; }

        public string Breadcrumb { get; set; }

        public IList<DropDownItem> ParentGroups { get; set; }

        public PictureModel PictureModel { get; set; }

        public List<DiyProjectModel> DiyProjects { get; set; }
    }

    public partial class DiyGroupLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }
    }
}
