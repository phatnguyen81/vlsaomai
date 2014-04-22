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
    public class Article : BaseEntity, ISlugSupported, ILocalizedEntity
    {
        private ICollection<ArticleToGroup> _articleToGroups;

        public string Title { get; set; }

        public string Short { get; set; }

        public string Full { get; set; }

        public bool Published { get; set; }

        public int PictureId { get; set; }

        public DateTime? StartDateUtc { get; set; }

        public DateTime? EndDateUtc { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public string SeName { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual ICollection<ArticleToGroup> ArticleToGroups
        {
            get { return _articleToGroups ?? (_articleToGroups = new List<ArticleToGroup>()); }
            protected set { _articleToGroups = value; }
        }
    }
}
