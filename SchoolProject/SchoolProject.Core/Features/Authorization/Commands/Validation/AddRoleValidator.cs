using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Validation
{
    public class AddRoleValidator:AbstractValidator<AddRoleCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationCodeService _authorizationService;

        public AddRoleValidator(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationCodeService authorizationService)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            ApplyValidationRules();
            ApplyCustomValidation();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.RoleName).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            
        }
        public void ApplyCustomValidation()
        {
            RuleFor(r => r.RoleName)
               .MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsRoleNameExist(Key))
               .WithMessage(_stringLocalizer[SharedResourcesKeys.RoleNameIsExist]);
        }
    }
}
