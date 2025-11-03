using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.SubjectFeatures.Commands.Model;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;


namespace SchoolProject.Core.Features.SubjectFeatures.Commands.Validation
{
    public class EditSubjectValidation : AbstractValidator<EditSubjectCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly ISubjectService _subjectService;

        public EditSubjectValidation(IStringLocalizer<SharedResources> stringLocalizer,
            ISubjectService subjectService)
        {
            _stringLocalizer = stringLocalizer;
            _subjectService = subjectService;
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
                 .MustAsync(async (model, Key, CancellationToken) => !await _subjectService.IsNameExistExcludeSelf(Key, model.SubID))
                 .WithMessage("This name is already found");
            RuleFor(r => r.SubjectNameEn)
                .MustAsync(async (model, Key, CancellationToken) => !await _subjectService.IsNameExistExcludeSelf(Key, model.SubID))
                .WithMessage("This name is already found");
        }
    }
}
