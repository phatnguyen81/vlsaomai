using System;
using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;

namespace Toi.Plugin.Misc.Projects.Domain
{
    public class Project : BaseEntity, ILocalizedEntity
    {
        public string Title { get; set; }

        public string Full { get; set; }

        public int PictureId { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

    }
}
