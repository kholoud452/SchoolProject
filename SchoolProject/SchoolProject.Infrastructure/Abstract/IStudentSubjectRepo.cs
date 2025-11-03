using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstract
{
    public interface IStudentSubjectRepo : IGenericRepositoryAsync<StudentSubject>
    {
        public Task<bool> IsStudentRegisterInSubject(int studentId, int subjectId);
        public Task<List<StudentSubject>> GetStudentSubjectsByIdAsync(int studentId);
        public Task<StudentSubject> GetStudentSubjectAsync(int studentId, int subjectId);

    }
}
