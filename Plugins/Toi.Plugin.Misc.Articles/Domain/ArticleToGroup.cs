using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;

namespace Toi.Plugin.Misc.Articles.Domain
{
    public class ArticleToGroup : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public int ArticleGroupId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is featured
        /// </summary>
        public bool IsHotArticle { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets the category
        /// </summary>
        public virtual ArticleGroup ArticleGroup { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual Article Article { get; set; }
    }
}
