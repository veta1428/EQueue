using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;

namespace Rey.EQueue.Web.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddSubject")]
        [Route("add-subject")]
        public async Task<int> AddTeacher([FromBody] AddTeacherCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpGet(Name = "GetSubjects")]
        [Route("subjects")]
        public Task<GetSubjectsQueryResult> GetSubjects(CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetSubjectsQuery(), cancellationToken);
        }

        [HttpGet(Name = "GetSubjects")]
        [Route("{id}")]
        public Task<SubjectModel> GetSubject(int id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetSubjectQuery(id), cancellationToken);
        }
    }
}
