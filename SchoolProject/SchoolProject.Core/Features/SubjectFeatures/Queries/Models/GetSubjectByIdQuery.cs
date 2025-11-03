using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.SubjectFeatures.Queries.Results;

namespace SchoolProject.Core.Features.SubjectFeatures.Queries.Models
{
    public class GetSubjectByIdQuery : IRequest<Response<GetSubjectResult>>
    {
        public int Id { get; set; }
        public GetSubjectByIdQuery(int id)
        {
            Id = id;
        }
    }
}
