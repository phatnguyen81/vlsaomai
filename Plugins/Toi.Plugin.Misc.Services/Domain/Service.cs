using System;
using Nop.Core;
using Nop.Core.Domain.Localization;

namespace Toi.Plugin.Misc.Services.Domain
{
    public class Service : BaseEntity, ILocalizedEntity
    {
        public string Title { get; set; }

        public string Full { get; set; }

        public int PictureId { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

    }
}
