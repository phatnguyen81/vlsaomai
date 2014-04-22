using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Admin.Models.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.Articles.Validators;

namespace Toi.Plugin.Misc.Articles.Models
{
    public partial class ArticleGroupListModel : BaseNopModel
    {
        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.List.SearchArticleGroupName")]
        [AllowHtml]
        public string SearchArticleGroupName { get; set; }
    }
    [Validator(typeof(ArticleGroupValidator))]
    public partial class ArticleGroupModel : BaseNopEntityModel, ILocalizedModel<ArticleGroupLocalizedModel>
    {
        public ArticleGroupModel()
        {
            if (PageSize < 1)
            {
                PageSize = 5;
            }
            Locales = new List<ArticleGroupLocalizedModel>();
        }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Parent")]
        public int ParentGroupId { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.PageSize")]
        public int PageSize { get; set; }


        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.PageSizeOptions")]
        public string PageSizeOptions { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.ShowOnHomePage")]
        public bool ShowOnHomePage { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Deleted")]
        public bool Deleted { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        public IList<ArticleGroupLocalizedModel> Locales { get; set; }

        public string Breadcrumb { get; set; }

        //Store mapping
        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.LimitedToStores")]
        public bool LimitedToStores { get; set; }
        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.AvailableStores")]
        public List<StoreModel> AvailableStores { get; set; }
        public int[] SelectedStoreIds { get; set; }


        public IList<DropDownItem> ParentGroups { get; set; }



    }

    public partial class ArticleGroupLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }
    }

}
