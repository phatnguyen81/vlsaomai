using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.UI.Paging;
using FluentValidation.Attributes;
using Toi.Plugin.Misc.Articles.Validators;

namespace Toi.Plugin.Misc.Articles.Models
{
    public partial class ArticleListModel : BaseNopModel
    {
        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.List.SearchArticleName")]
        [AllowHtml]
        public string SearchArticleName { get; set; }

        public BasePageableModel PagingFilteringContext { get; set; }

        public IList<ArticleModel> Articles { get; set; }
    }
    [Validator(typeof(ArticleValidator))]
    public class ArticleModel : BaseNopEntityModel, ILocalizedModel<ArticleLocalizedModel>
    {
        public ArticleModel()
        {
            Locales = new List<ArticleLocalizedModel>();
        }


        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.Short")]
        [AllowHtml]
        public string Short { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.StartDate")]
        [UIHint("DateTimeNullable")]
        public DateTime? StartDate { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.EndDate")]
        [UIHint("DateTimeNullable")]
        public DateTime? EndDate { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Picture")]
        public int PictureId { get; set; }

        public IList<ArticleLocalizedModel> Locales { get; set; }

        public int NumberOfAvailableGroups{get;set;}

        public class ArticleGroupModel : BaseNopEntityModel
        {
            public int ArticleId { get; set; }

            [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }

            [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.GroupName")]
            [UIHint("ArticleGroup")]
            public string ArticleGroup { get; set; }

            public int ArticleGroupId { get; set; }

            [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.Published")]
            public bool Published { get; set; }

            [NopResourceDisplayName("Toi.Plugin.Misc.Articles.ArticleGroup.Fields.IsHotArticle")]
            public bool IsHotArticle { get; set; }
        }
    }
    public partial class ArticleLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }
        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.Short")]
        [AllowHtml]
        public string Short { get; set; }
        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Articles.Article.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }
    }
}
