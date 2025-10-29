using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authantication.Command.Models
{
    public class SendResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
