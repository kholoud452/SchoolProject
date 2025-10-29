using MediatR;
using SchoolProject.Core.Bases;


namespace SchoolProject.Core.Features.DepartmentFeature.Commands.Models
{
    public class AddDepartmentCommand : IRequest<Response<string>>
    {
        public string DNameAr { get; set; }
        public string DNameEn { get; set; }
        public int InsManager { get; set; }
    }
}
