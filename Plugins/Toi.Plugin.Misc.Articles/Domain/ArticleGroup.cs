using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Stores;

namespace Toi.Plugin.Misc.Articles.Domain
{
    public class ArticleGroup : BaseEntity, ILocalizedEntity, ISlugSupported, IStoreMappingSupported
    {
        public virtual string SystemName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string SeName { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public int ParentGroupId { get; set; }

        public int PictureId { get; set; }

        public bool ShowOnHomePage { get; set; }

        public bool LimitedToStores { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
    }
}
