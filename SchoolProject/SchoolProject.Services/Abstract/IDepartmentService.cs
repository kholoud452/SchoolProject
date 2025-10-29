using SchoolProject.Data.Entities;

namespace SchoolProject.Services.Abstract
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task<bool> IsDepartmentExist(int id);
        public Task<string> AddDepartment(Department department);
        public Task<string> EditAsync(Department department);
        public Task<List<Department>> GetAllAsync();
        public Task<string> SoftDeleteAsync(int id);
        public Task<bool> IsNameExist(string name, int? currentDeptId = null);
        public Task<bool> IsInstructorIsManagerForDept(int insId, int? currentDeptId = null);
    }
}
