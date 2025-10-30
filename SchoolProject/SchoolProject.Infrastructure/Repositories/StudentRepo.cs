using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentRepo : GenericRepositoryAsync<Student>, IStudentRepo
    {
        private readonly DbSet<Student> _students;

        public StudentRepo(schoolDBContext context) : base(context)
        {
            _students = context.Set<Student>();
        }
        public async Task<List<Student>> GetAllAsync()
        {
            return await _students.Where(s => !s.IsDeleted).ToListAsync();
        }
    }
}
