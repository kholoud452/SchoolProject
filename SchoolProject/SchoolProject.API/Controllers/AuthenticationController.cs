
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Authantication.Command.Models;
using SchoolProject.Core.Features.Authantication.Query.Models;
using SchoolProject.Data.AppMetaData;


namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.AuthenticationRouting.Login)]
        public async Task<IActionResult> Login([FromForm] SignInCommand signInCommand)
        {
            return NewResult(await mediator.Send(signInCommand));
        }
        [HttpPost(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand refreshTokenCommand)
        {
            return NewResult(await mediator.Send(refreshTokenCommand));
        }
        [HttpPost(Router.AuthenticationRouting.ValidatorToken)]
        public async Task<IActionResult> ValidatorToken([FromQuery] GetAuthenticationQuery getAuthenticationQuery)
        {
            return NewResult(await mediator.Send(getAuthenticationQuery));
        }
        [HttpGet(Router.AuthenticationRouting.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery confirmEmailQuery)
        {
            return NewResult(await mediator.Send(confirmEmailQuery));
        }
        [HttpPost(Router.AuthenticationRouting.SendResetPasswordCode)]
        public async Task<IActionResult> SendResetPasswordCode([FromQuery] SendResetPasswordCommand sendResetPassword)
        {
            return NewResult(await mediator.Send(sendResetPassword));
        }
        [HttpGet(Router.AuthenticationRouting.ConfirmResetPassword)]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery resetPasswordQuery)
        {
            return NewResult(await mediator.Send(resetPasswordQuery));
        }
        [HttpPost(Router.AuthenticationRouting.ResetNewPassword)]
        public async Task<IActionResult> ResetNewPassword([FromForm] ResetPasswordCommand resetPasswordCommand)
        {
            return NewResult(await mediator.Send(resetPasswordCommand));
        }
    }
}
