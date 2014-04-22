using System;
using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;

namespace Toi.Plugin.Misc.DoItYourself.Domain
{
    public class DiyGroup : BaseEntity, ILocalizedEntity, ISlugSupported
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string SeName { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public int ParentGroupId { get; set; }

        public bool Deleted { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
    }
}
