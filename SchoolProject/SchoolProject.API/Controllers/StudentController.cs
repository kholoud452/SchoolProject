using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StudentController : AppControllerBase
    {
        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await mediator.Send(new GetStudentListQuery()));
        }
        [AllowAnonymous]
        [HttpGet(Router.StudentRouting.Pagination)]
        public async Task<IActionResult> Pagination([FromQuery] GetStudentPaginatedListQuery query)
        {
            return Ok(await mediator.Send(query));
        }
        [HttpGet(Router.StudentRouting.GetByID)]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            return NewResult(await mediator.Send(new GetStudentByIDQuery(id)));
        }
        [Authorize(Policy = "Create")]
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> Craete([FromBody] AddStudentCommand command)
        {
            return NewResult(await mediator.Send(command));
        }
        [Authorize(Policy = "Edit")]
        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand command)
        {
            return NewResult(await mediator.Send(command));
        }
        [Authorize(Policy = "Delete")]
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return NewResult(await mediator.Send(new DeleteStudentCommand(id)));
        }
    }
}
