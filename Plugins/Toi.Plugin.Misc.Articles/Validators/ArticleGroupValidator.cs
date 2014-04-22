using FluentValidation;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Articles.Models;

namespace Toi.Plugin.Misc.Articles.Validators
{
    class ArticleGroupValidator : AbstractValidator<ArticleGroupModel>
    {
        public ArticleGroupValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotNull().WithMessage(localizationService.GetResource("Admin.Plugins.ArticleManagement.Channels.Fields.Name.Required"));
        }
    }
}
