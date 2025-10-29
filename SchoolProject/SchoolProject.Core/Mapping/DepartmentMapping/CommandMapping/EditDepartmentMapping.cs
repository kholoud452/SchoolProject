using SchoolProject.Core.Features.DepartmentFeature.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void EditDepartmentMapping()
        {
            CreateMap<Department, EditDepartmentCommand>().ReverseMap();
        }
    }
}
