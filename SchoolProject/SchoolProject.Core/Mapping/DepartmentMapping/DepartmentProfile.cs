using AutoMapper;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            GetDepartmentByIdMapping();
            AddDepartmentMapping();
            EditDepartmentMapping();
            GetDepartmentListMapping();
        }
    }
}
