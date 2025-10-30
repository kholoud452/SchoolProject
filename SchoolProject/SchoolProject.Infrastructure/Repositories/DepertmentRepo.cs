using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class DepertmentRepo : GenericRepositoryAsync<Department>, IDepertmentRepo
    {
        private readonly schoolDBContext _context;

        public DepertmentRepo(schoolDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.Where(d => !d.IsDeleted).ToListAsync();
        }
    }
}
