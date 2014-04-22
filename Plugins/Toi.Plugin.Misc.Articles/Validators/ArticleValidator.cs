using FluentValidation;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Articles.Models;

namespace Toi.Plugin.Misc.Articles.Validators
{
    class ArticleValidator : AbstractValidator<ArticleModel>
    {
        public ArticleValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Articles.Article.Fields.Title.Required"));

            RuleFor(x => x.Short)
                .NotNull()
                .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Articles.Article.Fields.Short.Required"));

            RuleFor(x => x.Full)
                .NotNull()
                .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Articles.Article.Fields.Full.Required"));
        }
    }
}
