using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentSubjectRepo : GenericRepositoryAsync<StudentSubject>, IStudentSubjectRepo
    {
        private readonly schoolDBContext _context;

        public StudentSubjectRepo(schoolDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StudentSubject> GetStudentSubjectAsync(int studentId, int subjectId)
        {
            return await _context.StudentSubjects.FirstOrDefaultAsync(s => s.StudID == studentId && s.SubID == subjectId);
        }

        public async Task<List<StudentSubject>> GetStudentSubjectsByIdAsync(int studentId)
        {
            return await _context.StudentSubjects.Where(s => s.StudID == studentId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> IsStudentRegisterInSubject(int studentId, int subjectId)
        {
            return await _context.StudentSubjects.AnyAsync(s => s.StudID == studentId && s.SubID == subjectId);
        }
    }
}
