using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class InstructorRepo : GenericRepositoryAsync<Instructor>, IInstructorRepo
    {
        private readonly schoolDBContext _context;

        public InstructorRepo(schoolDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
