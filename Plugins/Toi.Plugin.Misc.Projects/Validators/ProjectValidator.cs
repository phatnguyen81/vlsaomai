using FluentValidation;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Projects.Models;

namespace Toi.Plugin.Misc.Projects.Validators
{
    public class ProjectValidator : AbstractValidator<ProjectModel>
    {
        public ProjectValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Projects.Project.Fields.Title.Required"));

            //RuleFor(x => x.Short)
            //    .NotNull()
            //    .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Projects.Project.Fields.Short.Required"));

            //RuleFor(x => x.Full)
            //    .NotNull()
            //    .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Projects.Project.Fields.Full.Required"));
        }
    }
}
