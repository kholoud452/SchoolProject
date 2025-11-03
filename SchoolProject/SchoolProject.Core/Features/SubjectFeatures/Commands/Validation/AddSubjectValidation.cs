using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.SubjectFeatures.Commands.Model;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.SubjectFeatures.Commands.Validation
{
    public class AddSubjectValidation : AbstractValidator<AddSubjectCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly ISubjectService _subjectService;

        public AddSubjectValidation(IStringLocalizer<SharedResources> stringLocalizer,
            ISubjectService subjectService)
        {
            _stringLocalizer = stringLocalizer;
            _subjectService = subjectService;
            ApplyValidationsRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(r => r.SubjectNameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.SubjectNameEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Requierd]);
            RuleFor(r => r.Period).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }

        public async Task ApplyCustomValidationRules()
        {
            RuleFor(r => r.SubjectNameAr)
                .MustAsync(async (Key, CancellationToken) => !await _subjectService.IsNameExist(Key))
                .WithMessage("This name is already found");
            RuleFor(r => r.SubjectNameEn)
                .MustAsync(async (Key, CancellationToken) => !await _subjectService.IsNameExist(Key))
                .WithMessage("This name is already found");
        }
    }
}
