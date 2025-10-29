using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authantication.Query.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
