using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpGet(Router.AuthorizationRouting.GetAll)]
        public async Task<IActionResult> AddRole()
        {
            return NewResult(await mediator.Send(new GetRoleListQuery()));
        }
        [HttpGet(Router.AuthorizationRouting.GetRoleById)]
        public async Task<IActionResult> GetRoleById([FromRoute] string id)
        {
            return NewResult(await mediator.Send(new GetRoleByIdQuery(id)));
        }
        [HttpGet(Router.AuthorizationRouting.ManageUserRoles)]
        public async Task<IActionResult> UserRoles([FromRoute] string id)
        {
            return NewResult(await mediator.Send(new ManageUserRolesQuery(id)));
        }
        [HttpPost(Router.AuthorizationRouting.AddRole)]
        public async Task<IActionResult> AddRole([FromForm] AddRoleCommand addRole)
        {
            return NewResult(await mediator.Send(addRole));
        }
        [HttpPut(Router.AuthorizationRouting.EditRole)]
        public async Task<IActionResult> EditRole([FromForm] EditRoleCommand editRole)
        {
            return NewResult(await mediator.Send(editRole));
        }
        [HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserClaimsCommend commend)
        {
            return NewResult(await mediator.Send(commend));
        }
        [HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommend commend)
        {
            return NewResult(await mediator.Send(commend));
        }
        [HttpDelete(Router.AuthorizationRouting.DeleteRole)]
        public async Task<IActionResult> DeleteRole([FromRoute] string id)
        {
            return NewResult(await mediator.Send(new DeleteRoleCommand(id)));
        }
        [HttpGet(Router.AuthorizationRouting.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims([FromRoute] string id)
        {
            return NewResult(await mediator.Send(new ManageUserClaimsQuery(id)));
        }
    }
}
