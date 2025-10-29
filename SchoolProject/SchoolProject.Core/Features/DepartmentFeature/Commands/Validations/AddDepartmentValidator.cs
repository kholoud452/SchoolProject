using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.DepartmentFeature.Commands.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.DepartmentFeature.Commands.Validations
{
    public class AddDepartmentValidator : AbstractValidator<AddDepartmentCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentService _departmentService;
        private readonly IInstructorService _instructorSevice;

        public AddDepartmentValidator(IStringLocalizer<SharedResources> stringLocalizer,
            IDepartmentService departmentService,
            IInstructorService instructorSevice)
        {
            _stringLocalizer = stringLocalizer;
            _departmentService = departmentService;
            _instructorSevice = instructorSevice;
            ApplyValidationsRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(r => r.DNameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.DNameEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);


        }

        public async Task ApplyCustomValidationRules()
        {
            RuleFor(r => r.DNameEn)
                .MustAsync(async (Key, CancellationToken) => !await _departmentService.IsNameExist(Key))
                .WithMessage("This name is already found");
            RuleFor(r => r.DNameAr)
                .MustAsync(async (Key, CancellationToken) => !await _departmentService.IsNameExist(Key))
                .WithMessage("This name is already found");
            RuleFor(r => r.InsManager)
                 .MustAsync(async (Key, CancellationToken) => await _instructorSevice.IsInstructorExist(Key))
                 .WithMessage(_stringLocalizer[SharedResourcesKeys.InstructorIsNotExist]);
            RuleFor(r => r.InsManager)
                .MustAsync(async (insId, cancellationToken) =>
                          !await _departmentService.IsInstructorIsManagerForDept(insId))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.InstructorIsAlreadyManagerForDept]);

        }
    }
}
