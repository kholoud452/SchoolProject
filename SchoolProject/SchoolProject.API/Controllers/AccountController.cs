using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Core.Features.User.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class AccountController : AppControllerBase
    {
        [HttpPost(Router.AccountRouting.Register)]
        public async Task<IActionResult> Register([FromBody] AddUserCommand userCommand)
        {
            return NewResult(await mediator.Send(userCommand));
        }
        [HttpGet(Router.AccountRouting.Pagination)]
        public async Task<IActionResult> Pagination([FromQuery] GitUserPaginationListQuery query)
        {
            return Ok(await mediator.Send(query));
        }
        [HttpGet(Router.AccountRouting.GetByID)]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            return NewResult(await mediator.Send(new GetUserByIdQuery(id)));
        }
        [HttpPut(Router.AccountRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] UpdateUserCommand command)
        {
            return NewResult(await mediator.Send(command));
        }
        [HttpDelete(Router.AccountRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            return NewResult(await mediator.Send(new DeleteUserCommand(id)));
        }
        [HttpPut(Router.AccountRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            return NewResult(await mediator.Send(command));
        }
    }
}
