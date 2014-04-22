using System;
using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;

namespace Toi.Plugin.Misc.DoItYourself.Domain
{
    public class DiyProject : BaseEntity, ISlugSupported, ILocalizedEntity
    {
        public string Title { get; set; }

        public string Short { get; set; }

        public string Full { get; set; }

        public bool Published { get; set; }

        public bool ShowOnHomePage { get; set; }

        public int PictureId { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public string SeName { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int DiyGroupId { get; set; }

        //public BranchGroup BranchGroup { get; set; }
    }
}
