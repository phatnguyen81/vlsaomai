using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;

namespace Toi.Plugin.Misc.Branches.Domain
{
    public class Branch : BaseEntity, ISlugSupported, ILocalizedEntity
    {
        public string Title { get; set; }

        public string Short { get; set; }

        public string Full { get; set; }

        public bool Published { get; set; }

        public int PictureId { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public string SeName { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public Decimal Latitude { get; set; }

        public Decimal Longitude { get; set; }

        public int BranchGroupId { get; set; }

        //public BranchGroup BranchGroup { get; set; }
    }
}
