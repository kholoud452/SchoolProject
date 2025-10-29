using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.ImplementAbstract
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepo _instructorRepo;

        public InstructorService(IInstructorRepo instructorRepo)
        {
            _instructorRepo = instructorRepo;
        }

        public async Task<bool> IsInstructorExist(int id)
        {
            return await _instructorRepo.GetTableNoTracking().AnyAsync(i => i.InsId.Equals(id));
        }
    }
}
