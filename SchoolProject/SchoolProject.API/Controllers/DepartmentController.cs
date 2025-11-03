using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.DepartmentFeature.Commands.Models;
using SchoolProject.Core.Features.DepartmentFeature.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouting.GetAll)]
        public async Task<IActionResult> GetDepartmentList()
        {
            return NewResult(await mediator.Send(new GetDepartmentListQuery()));
        }
        [HttpGet(Router.DepartmentRouting.GetByID)]
        public async Task<IActionResult> GetDepartmentByID([FromQuery] GetDepartmentByIDQuery query)
        {
            return NewResult(await mediator.Send(query));
        }
        [Authorize(Policy = "Create")]
        [HttpPost(Router.DepartmentRouting.Create)]
        public async Task<IActionResult> Craete([FromBody] AddDepartmentCommand command)
        {
            return NewResult(await mediator.Send(command));
        }
        [Authorize(Policy = "Edit")]
        [HttpPut(Router.DepartmentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditDepartmentCommand command)
        {
            return NewResult(await mediator.Send(command));
        }
        [Authorize(Policy = "Delete")]
        [HttpPatch(Router.DepartmentRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return NewResult(await mediator.Send(new DeleteDepartmentCommand(id)));
        }
    }
}
