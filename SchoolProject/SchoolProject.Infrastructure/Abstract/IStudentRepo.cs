using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Abstract
{
    public interface IStudentRepo:IGenericRepositoryAsync<Student>
    {
        public Task<List<Student>> GetAllAsync();
    }
}
