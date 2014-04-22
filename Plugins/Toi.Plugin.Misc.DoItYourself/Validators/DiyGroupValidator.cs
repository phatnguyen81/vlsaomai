using FluentValidation;
using Nop.Services.Localization;
using Toi.Plugin.Misc.DoItYourself.Models;

namespace Toi.Plugin.Misc.DoItYourself.Validators
{
    public class DiyGroupValidator : AbstractValidator<DiyGroupModel>
    {
        public DiyGroupValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotNull().WithMessage(localizationService.GetResource("Toi.Plugin.Misc.DoItYourself.DiyGroup.Fields.Name.Required"));
        }
    }
}
