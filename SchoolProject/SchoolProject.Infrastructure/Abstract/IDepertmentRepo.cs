using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstract
{
    public interface IDepertmentRepo : IGenericRepositoryAsync<Department>
    {
        public Task<List<Department>> GetAllAsync();
    }
}
