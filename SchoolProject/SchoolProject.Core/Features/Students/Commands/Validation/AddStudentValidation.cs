using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.Students.Commands.Validation
{
    public class AddStudentValidation : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentService _departmentService;

        public AddStudentValidation(IStudentService studentService,
            IStringLocalizer<SharedResources> stringLocalizer,
            IDepartmentService departmentService)
        {
            _studentService = studentService;
            _stringLocalizer = stringLocalizer ?? throw new ArgumentNullException(nameof(stringLocalizer));
            _departmentService = departmentService;
            ApplyValidationsRules();
             ApplyCustomValidationRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(r => r.NameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.NameEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.Address).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(r => r.DID).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }

        public async Task ApplyCustomValidationRules()
        {
            RuleFor(r => r.NameEn)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key))
                .WithMessage("This name is already found");
            RuleFor(r => r.NameAr)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key))
                .WithMessage("This name is already found");
            RuleFor(r => r.DID)
                .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentExist(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.DepartmentIsNotExist]);

        }
    }
}
