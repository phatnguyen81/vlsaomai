using FluentValidation;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Events.Models;

namespace Toi.Plugin.Misc.Events.Validators
{
    public class EventValidator : AbstractValidator<EventModel>
    {
        public EventValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Events.Event.Fields.Title.Required"));

            //RuleFor(x => x.Short)
            //    .NotNull()
            //    .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Events.Event.Fields.Short.Required"));

            //RuleFor(x => x.Full)
            //    .NotNull()
            //    .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Events.Event.Fields.Full.Required"));
        }
    }
}
