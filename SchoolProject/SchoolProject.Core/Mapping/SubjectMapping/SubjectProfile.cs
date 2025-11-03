using AutoMapper;

namespace SchoolProject.Core.Mapping.SubjectMapping
{
    public partial class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            AddSubjectMapper();
            EditSubjectMapper();
            GetSubjectMapping();
        }
    }
}
