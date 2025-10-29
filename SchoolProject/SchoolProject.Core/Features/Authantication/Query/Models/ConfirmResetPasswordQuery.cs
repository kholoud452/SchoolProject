using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authantication.Query.Models
{
    public class ConfirmResetPasswordQuery : IRequest<Response<string>>
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
