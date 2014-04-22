using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Core.Domain.Media;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.UI.Paging;
using FluentValidation.Attributes;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.Branches.Validators;

namespace Toi.Plugin.Misc.Branches.Models
{
    public partial class BranchListModel : BaseNopModel
    {
        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.List.SearchBranchName")]
        [AllowHtml]
        public string SearchBranchName { get; set; }

        public BasePageableModel PagingFilteringContext { get; set; }

        public IList<BranchModel> Branches { get; set; }
    }

    [Validator(typeof(BranchValidator))]
    public class BranchModel : BaseNopEntityModel, ILocalizedModel<BranchLocalizedModel>
    {
        public BranchModel()
        {
            Locales = new List<BranchLocalizedModel>();
        }


        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.Short")]
        [AllowHtml]
        public string Short { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.Latitude")]
        public Decimal Latitude { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.Longitude")]
        public Decimal Longitude { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Picture")]
        public int PictureId { get; set; }

        public IList<BranchLocalizedModel> Locales { get; set; }

        public int NumberOfAvailableGroups { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.BranchGroup")]
        public int BranchGroupId { get; set; }
        public IList<DropDownItem> BranchGroups { get; set; }

        public PictureModel PictureModel { get; set; }
    }
    public partial class BranchLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }
        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.Short")]
        [AllowHtml]
        public string Short { get; set; }
        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.Branch.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }
    }
}
