using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authantication.Command.Models;
using SchoolProject.Core.SharedResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authantication.Command.Validation
{
    public class SignInValidation:AbstractValidator<SignInCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public SignInValidation(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(r => r.UserName).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(r => r.Password).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
    }
}
