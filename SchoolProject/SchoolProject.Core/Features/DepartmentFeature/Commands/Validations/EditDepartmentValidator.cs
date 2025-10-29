using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.DepartmentFeature.Commands.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.DepartmentFeature.Commands.Validations
{
    public class EditDepartmentValidator : AbstractValidator<EditDepartmentCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentService _departmentService;
        private readonly IInstructorService _instructorService;

        public EditDepartmentValidator(IStringLocalizer<SharedResources> stringLocalizer,
            IDepartmentService departmentService,
            IInstructorService instructorService)
        {
            _stringLocalizer = stringLocalizer;
            _departmentService = departmentService;
            _instructorService = instructorService;
            ApplyValidationsRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(r => r.DID).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.DNameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.DNameEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
        }

        public async Task ApplyCustomValidationRules()
        {
            RuleFor(r => r.DNameEn)
                .MustAsync(async (cmd, Key, CancellationToken) => !await _departmentService.IsNameExist(Key, cmd.DID))
                .WithMessage("This name is already found");
            RuleFor(r => r.DNameAr)
                .MustAsync(async (cmd, Key, CancellationToken) => !await _departmentService.IsNameExist(Key, cmd.DID))
                .WithMessage("This name is already found");
            RuleFor(r => r.InsManager)
                .MustAsync(async (Key, CancellationToken) => await _instructorService.IsInstructorExist(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.InstructorIsNotExist]);
            RuleFor(r => r.InsManager)
                .MustAsync(async (cmd, insId, cancellationToken) =>
                          !await _departmentService.IsInstructorIsManagerForDept(insId, cmd.DID))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.InstructorIsAlreadyManagerForDept]);

        }
    }
}
