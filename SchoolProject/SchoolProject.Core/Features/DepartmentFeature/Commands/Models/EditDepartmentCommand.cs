using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.DepartmentFeature.Commands.Models
{
    public class EditDepartmentCommand : IRequest<Response<string>>
    {
        public int DID { get; set; }
        public string DNameAr { get; set; }
        public string DNameEn { get; set; }
        public int InsManager { get; set; }
    }
}
