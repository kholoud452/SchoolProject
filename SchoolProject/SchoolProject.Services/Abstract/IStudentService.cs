using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;

namespace SchoolProject.Services.Abstract
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllAsync();
        public Task<Student> GetStudentByIDAsync(int id);
        public Task<string> EditAsync(Student student);
        public Task<string> AddStudent(Student student);
        public Task<string> AddStudentSubject(int studentId, List<int> subjectsId);
        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameExistExcludeSelf(string name, int id);
        public Task<string> DeleteAsync(Student student);
        public IQueryable<Student> GetAlLQuarableStudents();
        public IQueryable<Student> GetAlLQuarableByDepartmentStudents(int DID);
        public IQueryable<Student> FilterStudentPaginationQuarable(StudentOrderingEnum orderBy, string search);

    }
}
