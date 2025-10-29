using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authantication.Query.Models;
using SchoolProject.Core.SharedResource;

namespace SchoolProject.Core.Features.Authantication.Query.Validator
{
    public class ConfirmResetPasswordQueryValidator : AbstractValidator<ConfirmResetPasswordQuery>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ConfirmResetPasswordQueryValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.Code).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(r => r.Email).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

        }
    }
}
