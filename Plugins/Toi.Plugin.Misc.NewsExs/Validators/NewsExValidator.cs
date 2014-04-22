using FluentValidation;
using Nop.Services.Localization;
using Toi.Plugin.Misc.NewsExs.Models;

namespace Toi.Plugin.Misc.NewsExs.Validators
{
    public class NewsExValidator : AbstractValidator<NewsExModel>
    {
        public NewsExValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Title.Required"));

            //RuleFor(x => x.Short)
            //    .NotNull()
            //    .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Short.Required"));

            //RuleFor(x => x.Full)
            //    .NotNull()
            //    .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.NewsExs.NewsEx.Fields.Full.Required"));
        }
    }
}
