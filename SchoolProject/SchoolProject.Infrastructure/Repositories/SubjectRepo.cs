using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class SubjectRepo : GenericRepositoryAsync<Subject>, ISubjrctRepo
    {
        private readonly schoolDBContext _context;

        public SubjectRepo(schoolDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
