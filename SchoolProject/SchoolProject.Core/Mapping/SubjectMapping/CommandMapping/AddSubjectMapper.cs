using SchoolProject.Core.Features.SubjectFeatures.Commands.Model;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.SubjectMapping
{
    public partial class SubjectProfile
    {
        public void AddSubjectMapper()
        {
            CreateMap<Subject, AddSubjectCommand>().ReverseMap();
        }
    }
}
