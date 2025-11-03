using SchoolProject.Core.Features.SubjectFeatures.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.SubjectMapping
{
    public partial class SubjectProfile
    {
        public void GetSubjectMapping()
        {
            CreateMap<GetSubjectResult, Subject>().ReverseMap();
        }
    }
}
