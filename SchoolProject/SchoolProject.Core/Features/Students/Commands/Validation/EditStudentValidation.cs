using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.Students.Commands.Validation
{
    public class EditStudentValidation : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentService _studentService;

        public EditStudentValidation(IStudentService studentService)
        {
            ApplyValidationsRules();
            // ApplyCustomValidationRules();
            _studentService = studentService;
        }
        public void ApplyValidationsRules()
        {
            RuleFor(r => r.NameEn).NotEmpty().WithMessage("Name must not be empty.")
                                .NotNull().WithMessage("Name must not be null.");
            RuleFor(r => r.NameAr).NotEmpty().WithMessage("Name must not be empty.")
                                .NotNull().WithMessage("Name must not be null.");
            RuleFor(r => r.Address).NotEmpty().WithMessage("{PropertyName} must not be empty.")
                                .NotNull().WithMessage("{PropertyValue} must not be null.");
        }

        public async Task ApplyCustomValidationRules()
        {
            RuleFor(r => r.NameEn)
                .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameExistExcludeSelf(Key, model.StudID))
                .WithMessage("This name is already found");
            RuleFor(r => r.NameAr)
                .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameExistExcludeSelf(Key, model.StudID))
                .WithMessage("This name is already found");
        }
    }
}
