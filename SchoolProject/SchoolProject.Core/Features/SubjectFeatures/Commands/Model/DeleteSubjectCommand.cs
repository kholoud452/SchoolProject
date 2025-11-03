using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.SubjectFeatures.Commands.Model
{
    public class DeleteSubjectCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteSubjectCommand(int id)
        {
            Id = id;
        }
    }
}
