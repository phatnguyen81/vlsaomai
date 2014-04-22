using FluentValidation;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Services.Models;
using Toi.Plugin.Misc.Services.Models;

namespace Toi.Plugin.Misc.Services.Validators
{
    public class ServiceValidator : AbstractValidator<ServiceModel>
    {
        public ServiceValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Services.Service.Fields.Title.Required"));

            //RuleFor(x => x.Short)
            //    .NotNull()
            //    .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Services.Service.Fields.Short.Required"));

            //RuleFor(x => x.Full)
            //    .NotNull()
            //    .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Services.Service.Fields.Full.Required"));
        }
    }
}
