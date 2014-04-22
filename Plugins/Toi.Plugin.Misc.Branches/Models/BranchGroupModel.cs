using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Core.Domain.Media;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Telerik.Web.Mvc.UI;
using Toi.Plugin.Misc.Branches.Validators;

namespace Toi.Plugin.Misc.Branches.Models
{
    public partial class BranchGroupListModel : BaseNopModel
    {
        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.List.SearchBranchGroupName")]
        [AllowHtml]
        public string SearchBranchGroupName { get; set; }
    }

    [Validator(typeof(BranchGroupValidator))]
    public class BranchGroupModel : BaseNopEntityModel, ILocalizedModel<BranchGroupLocalizedModel>
    {
        public BranchGroupModel()
        {
            Locales = new List<BranchGroupLocalizedModel>();
        }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Parent")]
        public int ParentGroupId { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Deleted")]
        public bool Deleted { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        public IList<BranchGroupLocalizedModel> Locales { get; set; }

        public string Breadcrumb { get; set; }

        public IList<DropDownItem> ParentGroups { get; set; }

        public PictureModel PictureModel { get; set; }

        public List<BranchModel> Branches { get; set; }
    }

    public partial class BranchGroupLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Branches.BranchGroup.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }
    }
}
