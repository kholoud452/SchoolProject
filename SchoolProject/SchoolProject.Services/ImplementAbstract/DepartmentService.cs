using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.ImplementAbstract
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepertmentRepo _depertmentRepo;
        private readonly schoolDBContext _context;

        public DepartmentService(IDepertmentRepo depertmentRepo, schoolDBContext context)
        {
            _depertmentRepo = depertmentRepo;
            _context = context;
        }

        public async Task<string> AddDepartment(Department department)
        {
            if (await IsNameExist(department.DNameEn) == true)
                return "Exist";
            var addedDepartment = await _depertmentRepo.AddAsync(department);
            return "Success";
        }

        public async Task<string> SoftDeleteAsync(int id)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var department = await GetDepartmentByIdAsync(id);
                if (department == null || department.IsDeleted)
                    return "DepartmentNotFoundOrDeleted";

                foreach (var student in department.Students)
                    student.DID = null;

                foreach (var instructor in department.Instructors)
                    instructor.DID = null;

                foreach (var dept_sub in department.DepartmentSubjects)
                    dept_sub.IsDeleted = true;

                department.IsDeleted = true;

                await _depertmentRepo.UpdateAsync(department);

                await transaction.CommitAsync();
                return "Department deleted successfully";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<string> EditAsync(Department department)
        {
            await _depertmentRepo.UpdateAsync(department);
            return "Success";
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _depertmentRepo.GetByIdAsync(id);
        }

        public async Task<bool> IsDepartmentExist(int id)
        {
            return await _depertmentRepo.GetTableNoTracking().AnyAsync(d => d.DID.Equals(id) && !d.IsDeleted);
        }

        public async Task<bool> IsInstructorIsManagerForDept(int insId, int? currentDeptId = null)
        {
            return await _depertmentRepo.GetTableNoTracking()
                .AnyAsync(i => i.InsManager == insId && i.DID != currentDeptId);
        }

        public async Task<bool> IsNameExist(string name, int? currentDeptId = null)
        {
            if (await _depertmentRepo.GetTableNoTracking()
                .Where(d => d.DNameEn == name && d.DID != currentDeptId)
                .FirstOrDefaultAsync() == null)
                return false;

            return true;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            var deptList = await _depertmentRepo.GetAllAsync();
            if (deptList.Count == 0)
                new List<Department>();
            return deptList;
        }
    }
}
