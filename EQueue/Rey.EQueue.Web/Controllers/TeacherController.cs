using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;

namespace Rey.EQueue.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TeacherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddTeacher")]
        [Route("add-teacher")]
        public async Task<int> AddTeacher([FromBody] AddTeacherCommand teacher, CancellationToken cancellationToken)
        {
            return await _mediator.Send(teacher, cancellationToken);
        }

        [HttpGet(Name = "GetTeachers")]
        [Route("teachers")]
        public async Task<GetTeachersQueryResult> GetTeachers(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTeachersQuery(), cancellationToken);
        }

        [HttpGet(Name = "GetTeacher")]
        [Route("teacher/{id}")]
        public async Task<TeacherModel> GetTeacher(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTeacherQuery(id), cancellationToken);
        }
    }
}
