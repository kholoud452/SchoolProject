using SchoolProject.Core.Features.DepartmentFeature.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void AddDepartmentMapping()
        {
            CreateMap<Department, AddDepartmentCommand>().ReverseMap();
        }
    }
}
