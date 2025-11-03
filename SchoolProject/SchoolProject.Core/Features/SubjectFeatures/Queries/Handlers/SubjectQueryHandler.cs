using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.SubjectFeatures.Queries.Models;
using SchoolProject.Core.Features.SubjectFeatures.Queries.Results;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.SubjectFeatures.Queries.Handlers
{
    public class SubjectQueryHandler : ResponseHandler,
        IRequestHandler<GetSubjectByIdQuery, Response<GetSubjectResult>>,
        IRequestHandler<GetSubjectsListQuery, Response<List<GetSubjectResult>>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            ISubjectService subjectService, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _subjectService = subjectService;
            _mapper = mapper;
        }

        public async Task<Response<GetSubjectResult>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var subjectFromDB = await _subjectService.GetById(request.Id);
            if (subjectFromDB == null)
                return BadRequest<GetSubjectResult>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var subjectMapper = _mapper.Map<GetSubjectResult>(subjectFromDB);
            var result = Success(subjectMapper);
            result.Meta = new { Operation = "Success" };
            return result;
        }

        public async Task<Response<List<GetSubjectResult>>> Handle(GetSubjectsListQuery request, CancellationToken cancellationToken)
        {
            var subjectListFromDB = await _subjectService.GetAll();
            var subjectListMapper = _mapper.Map<List<GetSubjectResult>>(subjectListFromDB);
            var result = Success(subjectListMapper);
            result.Meta = new { Operation = "Success" };
            return result;
        }
    }
}
