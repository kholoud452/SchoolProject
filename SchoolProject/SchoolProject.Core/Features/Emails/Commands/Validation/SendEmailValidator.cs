using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Core.SharedResource;

namespace SchoolProject.Core.Features.Emails.Commands.Validation
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public SendEmailValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.Message).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);

        }
    }
}
