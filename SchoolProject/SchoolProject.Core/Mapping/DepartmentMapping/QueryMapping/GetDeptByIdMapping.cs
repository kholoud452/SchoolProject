using SchoolProject.Core.Features.DepartmentFeature.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {

            CreateMap<Department, GetDepartmentResponse>().AfterMap((src, dist) =>
            {
                dist.Name = src != null ? src.Localize(src.DNameAr, src.DNameEn) : "No Department";
                dist.ManagerName = src != null ? src.Localize(src.instructor.NameAr, src.instructor.NameEn) : "No Manager";

                //dist.studentList = src != null ? src.Students.Select(s => new StudentResponse
                //{
                //    Name = s.Localize(s.NameAr, s.NameEn),
                //    Phone = s.Phone
                //}).ToList():new List<StudentResponse>() ;

                dist.instructorList = src != null ? src.Instructors.Select(i => new InstructorResponse
                {
                    Name = i.Localize(i.NameAr, i.NameEn),
                    Position = i.Position
                }).ToList() : new List<InstructorResponse>();

                dist.subjectList = src != null ? src.DepartmentSubjects.Select(sb => new SubjectResponse
                {
                    Name = sb.Localize(sb.Subject.SubjectNameAr, sb.Subject.SubjectNameEn),
                    Period = sb.Subject.Period
                }).ToList() : new List<SubjectResponse>();

            }).ReverseMap();
        }
    }
}
