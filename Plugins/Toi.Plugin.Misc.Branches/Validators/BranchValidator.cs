using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Nop.Services.Localization;
using Toi.Plugin.Misc.Branches.Models;

namespace Toi.Plugin.Misc.Branches.Validators
{
    public class BranchValidator : AbstractValidator<BranchModel>
    {
        public BranchValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Branches.Branch.Fields.Title.Required"));

            RuleFor(x => x.Short)
                .NotNull()
                .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Branches.Branch.Fields.Short.Required"));

            RuleFor(x => x.Full)
                .NotNull()
                .WithMessage(localizationService.GetResource("Toi.Plugin.Misc.Branches.Branch.Fields.Full.Required"));
        }
    }
}
