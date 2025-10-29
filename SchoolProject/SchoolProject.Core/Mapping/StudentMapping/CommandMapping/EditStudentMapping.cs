using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void EditStudentMapping()
        {
            CreateMap<Student, EditStudentCommand>().ReverseMap();
        }
    }
}
