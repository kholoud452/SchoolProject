using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResults>>
    {
        public string UserId { get; set; }
        public ManageUserClaimsQuery(string id)
        {
            UserId = id;
        }
    }
}
