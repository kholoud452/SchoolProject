using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResults>>
    {
        public string UserId { get; set; }
        public ManageUserRolesQuery(string id)
        {
            UserId = id;
        }
    }
}
