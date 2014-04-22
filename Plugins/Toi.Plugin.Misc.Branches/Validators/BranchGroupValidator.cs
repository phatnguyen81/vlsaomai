using FluentValidation;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Branches.Models;

namespace Toi.Plugin.Misc.Branches.Validators
{
    class BranchGroupValidator : AbstractValidator<BranchGroupModel>
    {
        public BranchGroupValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotNull().WithMessage(localizationService.GetResource("Admin.Plugins.ArticleManagement.Channels.Fields.Name.Required"));
        }
    }
}
