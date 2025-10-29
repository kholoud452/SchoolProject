using SchoolProject.Core.Features.DepartmentFeature.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentListMapping()
        {
            CreateMap<Department, GetDepartmentListResponse>().AfterMap((src, dist) =>
            {
                dist.Name = src != null ? src.Localize(src.DNameAr, src.DNameEn) : "No Department";
                dist.ManagerName = src?.instructor != null ? src.Localize(src.instructor.NameAr, src.instructor.NameEn) : "No Manager";
            }).ReverseMap();
        }
    }
}
