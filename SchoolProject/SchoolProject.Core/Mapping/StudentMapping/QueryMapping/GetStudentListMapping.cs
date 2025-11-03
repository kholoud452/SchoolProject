using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void GetStudentListMapping()
        {
            CreateMap<Student, GetStudentListResponse>().AfterMap((src, dist) =>
            {
                dist.DName = src?.Department != null ? src.Localize(src.Department.DNameAr, src.Department.DNameEn) : "No Department";
                dist.Name = src != null ? src.Localize(src.NameAr, src.NameEn) : "No Student";
            }).ReverseMap();
        }
    }
}
