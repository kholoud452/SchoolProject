using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.DepartmentFeature.Queries.Models;
using SchoolProject.Core.Features.DepartmentFeature.Queries.Results;
using SchoolProject.Core.SharedResource;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Services.Abstract;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.DepartmentFeature.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
        IRequestHandler<GetDepartmentByIDQuery, Response<GetDepartmentResponse>>,
        IRequestHandler<GetDepartmentListQuery, Response<List<GetDepartmentListResponse>>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;

        public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IDepartmentService departmentService, IMapper mapper,
            IStudentService studentService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _departmentService = departmentService;
            _mapper = mapper;
            _studentService = studentService;
        }

        public async Task<Response<GetDepartmentResponse>> Handle(GetDepartmentByIDQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(request.Id);
            if (department == null)
                return NotFound<GetDepartmentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var deptMapper = _mapper.Map<GetDepartmentResponse>(department);

            Expression<Func<Student, StudentResponse>> expressionStud =
               e => new StudentResponse(e.Localize(e.NameAr, e.NameEn), e.Phone);
            var studentList = _studentService.GetAlLQuarableByDepartmentStudents(request.Id);
            var PaginatedList = await studentList.Select(expressionStud).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            deptMapper.studentList = PaginatedList;


            var result = Success(deptMapper);
            result.Meta = new { Operation = "Success" };
            return result;

        }

        public async Task<Response<List<GetDepartmentListResponse>>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var deptList = await _departmentService.GetAllAsync();
            var deptMapper = _mapper.Map<List<GetDepartmentListResponse>>(deptList);
            var result = Success(deptMapper);
            result.Meta = new { Operation = "Success" };
            return result;
        }
    }
}
