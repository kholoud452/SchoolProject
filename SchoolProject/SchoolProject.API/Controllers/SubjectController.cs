
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.SubjectFeatures.Commands.Model;
using SchoolProject.Core.Features.SubjectFeatures.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SubjectController : AppControllerBase
    {
        [HttpGet(Router.SubjectRouting.GetAll)]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await mediator.Send(new GetSubjectsListQuery()));
        }
        [HttpGet(Router.SubjectRouting.GetByID)]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            return NewResult(await mediator.Send(new GetSubjectByIdQuery(id)));
        }
        [Authorize(Policy = "Create")]
        [HttpPost(Router.SubjectRouting.Create)]
        public async Task<IActionResult> Craete([FromBody] AddSubjectCommand command)
        {
            return NewResult(await mediator.Send(command));
        }
        [Authorize(Policy = "Edit")]
        [HttpPut(Router.SubjectRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditSubjectCommand command)
        {
            return NewResult(await mediator.Send(command));
        }
        [Authorize(Policy = "Delete")]
        [HttpPatch(Router.SubjectRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return NewResult(await mediator.Send(new DeleteSubjectCommand(id)));
        }
    }
}
