using MediatR;
using SchoolProject.Core.Bases;


namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class AddStudentSubjectsCommand : IRequest<Response<string>>
    {
        public int StudentId { get; set; }
        public List<int> SubjectsId { get; set; }
    }
}
