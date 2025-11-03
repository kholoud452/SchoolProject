using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class DepartmentSubjectRepo : GenericRepositoryAsync<DepartmentSubject>, IDepartmentSubjectRepo
    {
        private readonly schoolDBContext _context;

        public DepartmentSubjectRepo(schoolDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
