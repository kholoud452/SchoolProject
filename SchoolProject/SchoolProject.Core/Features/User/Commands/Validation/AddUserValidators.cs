using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Core.SharedResource;

namespace SchoolProject.Core.Features.User.Commands.Validation
{
    public class AddUserValidators : AbstractValidator<AddUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddUserValidators(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidations();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(r => r.FullName).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.Email).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.UserName).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(r => r.Password).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(r => r.ConfirmPassword).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .Matches(p => p.Password).WithMessage(_stringLocalizer[SharedResourcesKeys.PassNotMatchConfirmPass])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
        public void ApplyCustomValidations()
        {

        }


    }
}
