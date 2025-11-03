using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.SubjectFeatures.Commands.Model;
using SchoolProject.Core.SharedResource;
using SchoolProject.Data.Entities;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.SubjectFeatures.Commands.Handlers
{
    public class SubjectCommandHandler : ResponseHandler,
        IRequestHandler<AddSubjectCommand, Response<string>>,
        IRequestHandler<EditSubjectCommand, Response<string>>,
        IRequestHandler<DeleteSubjectCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            ISubjectService subjectService, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _subjectService = subjectService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            var subjectMapper = _mapper.Map<Subject>(request);
            var result = await _subjectService.Add(subjectMapper);
            if (result == "Success") return Created("");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditSubjectCommand request, CancellationToken cancellationToken)
        {
            var subjectFromDB = await _subjectService.GetById(request.SubID);
            if (subjectFromDB == null)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var subjectMapper = _mapper.Map(request, subjectFromDB);
            var result = await _subjectService.Update(subjectMapper);
            if (result == "Success") return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var deletedSubject = await _subjectService.Delete(request.Id);
            if (deletedSubject == "NotFound")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            else if (deletedSubject == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
            else return BadRequest<string>();
        }
    }
}
