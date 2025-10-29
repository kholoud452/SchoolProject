using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.DepartmentFeature.Queries.Results;

namespace SchoolProject.Core.Features.DepartmentFeature.Queries.Models
{
    public class GetDepartmentByIDQuery : IRequest<Response<GetDepartmentResponse>>
    {
        public int Id { get; set; }
        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }
    }
}
