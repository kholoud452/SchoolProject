using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.DepartmentFeature.Queries.Results;

namespace SchoolProject.Core.Features.DepartmentFeature.Queries.Models
{
    public class GetDepartmentListQuery : IRequest<Response<List<GetDepartmentListResponse>>>
    {
    }
}
