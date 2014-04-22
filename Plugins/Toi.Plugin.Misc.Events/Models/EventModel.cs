using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Toi.Plugin.Misc.Events.Models;
using Toi.Plugin.Misc.Events.Validators;

namespace Toi.Plugin.Misc.Events.Models
{
    public partial class EventListModel : BaseNopModel
    {
        [NopResourceDisplayName("Toi.Plugin.Misc.Events.Event.List.SearchEventName")]
        [AllowHtml]
        public string SearchEventName { get; set; }

        public IList<EventModel> Events { get; set; }
    }

    [Validator(typeof(EventValidator))]
    public class EventModel : BaseNopEntityModel, ILocalizedModel<EventLocalizedModel>
    {
        public EventModel()
        {
            Locales = new List<EventLocalizedModel>();
            PictureModel = new PictureModel();
        }


        [NopResourceDisplayName("Toi.Plugin.Misc.Events.Event.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Events.Event.Fields.Short")]
        public string Short { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Events.Event.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }

      
        [NopResourceDisplayName("Toi.Plugin.Misc.Events.Event.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Toi.Plugin.Misc.Events.Event.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Events.Event.Fields.Picture")]
        public int DisplayOrder { get; set; }

        public PictureModel PictureModel { get; set; }
        public IList<EventLocalizedModel> Locales { get; set; }
    }
    public partial class EventLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Events.Event.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }

        [NopResourceDisplayName("Toi.Plugin.Misc.Events.Event.Fields.Short")]
        [AllowHtml]
        public string Short { get; set; }


        [NopResourceDisplayName("Toi.Plugin.Misc.Events.Event.Fields.Full")]
        [AllowHtml]
        public string Full { get; set; }



    
    }
}
