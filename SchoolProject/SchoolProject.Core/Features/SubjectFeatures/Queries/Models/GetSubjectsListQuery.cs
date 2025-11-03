using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.SubjectFeatures.Queries.Results;

namespace SchoolProject.Core.Features.SubjectFeatures.Queries.Models
{
    public class GetSubjectsListQuery : IRequest<Response<List<GetSubjectResult>>>
    {
    }
}
