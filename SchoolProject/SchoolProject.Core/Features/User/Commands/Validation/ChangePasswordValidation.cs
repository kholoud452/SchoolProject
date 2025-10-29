using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Core.SharedResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.User.Commands.Validation
{
    public class ChangePasswordValidation:AbstractValidator<ChangePasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ChangePasswordValidation(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.CurrentPassword).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.NewPassword).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(r => r.ConfirmNewPassword).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .Matches(p => p.NewPassword).WithMessage(_stringLocalizer[SharedResourcesKeys.PassNotMatchConfirmPass])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
    }
}
