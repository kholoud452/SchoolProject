using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authantication.Command.Models;
using SchoolProject.Core.SharedResource;

namespace SchoolProject.Core.Features.Authantication.Command.Validation
{
    public class SendResetPasswordCommandValidator : AbstractValidator<SendResetPasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public SendResetPasswordCommandValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

        }

    }
}
