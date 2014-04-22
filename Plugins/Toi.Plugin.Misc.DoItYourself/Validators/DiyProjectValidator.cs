using FluentValidation;
using Nop.Services.Localization;
using Toi.Plugin.Misc.DoItYourself.Models;

namespace Toi.Plugin.Misc.DoItYourself.Validators
{
    public class DiyProjectValidator : AbstractValidator<DiyProjectModel>
    {
        public DiyProjectValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Title.Required"));

            //RuleFor(x => x.Short)
            //    .NotNull()
            //    .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Short.Required"));

            //RuleFor(x => x.Full)
            //    .NotNull()
            //    .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.DoItYourself.DiyProject.Fields.Full.Required"));
        }
    }
}
