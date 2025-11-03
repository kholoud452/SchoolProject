using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class InstructorSubjectRepo : GenericRepositoryAsync<Ins_Subject>, IInstructorSubjectRepo
    {
        private readonly schoolDBContext _context;

        public InstructorSubjectRepo(schoolDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
