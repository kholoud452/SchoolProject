
using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authantication.Command.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
