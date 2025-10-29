using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authantication.Command.Models;
using SchoolProject.Core.SharedResource;

namespace SchoolProject.Core.Features.Authantication.Command.Validation
{
    public class ResetPsswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ResetPsswordCommandValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            ApplyValidationsRules();
            _stringLocalizer = stringLocalizer;
        }
        public void ApplyValidationsRules()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.NewPassword).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(r => r.ConfirmNewPassword).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .Matches(p => p.NewPassword).WithMessage(_stringLocalizer[SharedResourcesKeys.PassNotMatchConfirmPass])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
    }
}
