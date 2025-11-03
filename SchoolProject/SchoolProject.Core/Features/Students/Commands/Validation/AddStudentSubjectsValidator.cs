using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.SharedResource;

namespace SchoolProject.Core.Features.Students.Commands.Validation
{
    public class AddStudentSubjectsValidator : AbstractValidator<AddStudentSubjectsCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddStudentSubjectsValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(r => r.StudentId).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
        }
    }
}
