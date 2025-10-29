using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.DepartmentFeature.Queries.Results
{
    public class GetDepartmentResponse
    {
        public string? Name { get; set; }
        public string? ManagerName { get; set; }
        public PaginatedResult<StudentResponse> studentList { get; set; }
        public List<SubjectResponse> subjectList { get; set; }
        public List<InstructorResponse> instructorList { get; set; }
    }

    public class StudentResponse
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }

        public StudentResponse(string? name, string? phone)
        {
            Name = name;
            Phone = phone;
        }
    }
    public class SubjectResponse
    {
        public string? Name { get; set; }
        public int? Period { get; set; }

    }
    public class InstructorResponse
    {
        public string? Name { get; set; }
        public string? Position { get; set; }


    }
}
