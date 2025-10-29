using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.Authorization.Commands.Validation
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationCodeService _authorizationService;

        public EditRoleValidator(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationCodeService authorizationService)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            ApplyValidationRules();
            ApplyCustomValidation();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(r => r.Name).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

        }
        public void ApplyCustomValidation()
        {
            RuleFor(r => r.Name)
               .MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsRoleNameExist(Key))
               .WithMessage(_stringLocalizer[SharedResourcesKeys.RoleNameIsExist]);
        }
    }
}
