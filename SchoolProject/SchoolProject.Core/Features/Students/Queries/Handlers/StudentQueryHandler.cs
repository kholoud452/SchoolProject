using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.SharedResource;
using SchoolProject.Core.Wrappers;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
        IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
        IRequestHandler<GetStudentByIDQuery, Response<GetStudentListResponse>>,
        IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizar;

        public StudentQueryHandler(IStudentService studentService,
            IMapper mapper, IStringLocalizer<SharedResources> stringLocalizar) : base(stringLocalizar)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizar = stringLocalizar;
        }

        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studList = await _studentService.GetAllAsync();
            var studListMapper = _mapper.Map<List<GetStudentListResponse>>(studList);
            var result = Success(studListMapper);
            result.Meta = new { Count = studListMapper.Count() };
            return result;
        }

        public async Task<Response<GetStudentListResponse>> Handle(GetStudentByIDQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIDAsync(request.Id);
            if (student == null)
                return NotFound<GetStudentListResponse>(_stringLocalizar[SharedResourcesKeys.NotFound]);

            var studentMapper = _mapper.Map<GetStudentListResponse>(student);
            var result = Success(studentMapper);
            result.Meta = new { Operation = "Success" };
            return result;
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            //Expression<Func<Student, GetStudentPaginatedListResponse>> expression =
            //    e => new GetStudentPaginatedListResponse(e.StudID, e.Localize(e.NameAr, e.NameEn),
            //    e.Address, e.Localize(e.Department.DNameAr, e.Department.DNameEn));
            // var querable = _studentService.GetAlLQuarableStudents();
            var filteredQuery = _studentService.FilterStudentPaginationQuarable(request.OrderBy, request.Search);
            var PaginatedList = await filteredQuery
                .Select(e => new GetStudentPaginatedListResponse(e.StudID, e.Localize(e.NameAr, e.NameEn),
                e.Address, e.Localize(e.Department.DNameAr, e.Department.DNameEn)))
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            return PaginatedList;
        }
    }
}
