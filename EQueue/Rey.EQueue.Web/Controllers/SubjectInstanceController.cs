using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;

namespace Rey.EQueue.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectInstanceController : ControllerBase
    {
        private IMediator _mediator;

        public SubjectInstanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddSubjectInstance")]
        [Route("add")]
        public async Task<int> AddSubjectInstance([FromBody] AddSubjectInstanceCommand subjectInstance, CancellationToken cancellationToken)
        {
            return await _mediator.Send(subjectInstance, cancellationToken);
        }

        [HttpGet(Name = "GetSubjectInstanceByTeacher")]
        [Route("teacher/{teacherId}")]
        public async Task<GetSubjectInstancesQueryResult> GetSubjectInstanceByTeacher(int teacherId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetSubjectInstancesByTeacherQuery(teacherId), cancellationToken);
        }

        [HttpGet(Name = "GetSubjectInstanceBySubject")]
        [Route("subject/{subjectId}")]
        public async Task<GetSubjectInstancesQueryResult> GetSubjectInstanceBySubject(int subjectId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetSubjectInstancesBySubjectQuery(subjectId), cancellationToken);
        }

        [HttpGet(Name = "GetSubjectInstances")]
        [Route("all")]
        public async Task<GetSubjectInstancesQueryResult> GetSubjectInstances(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetSubjectInstancesQuery(), cancellationToken);
        }

        [HttpGet(Name = "GetSubjectInstances")]
        [Route("subject-instance/{id}")]
        public async Task<SubjectInstanceDetailedModel> GetSubjectInstance(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetSubjectInstanceQuery(id), cancellationToken);
        }

        [HttpPost]
        [Route("add-timetable")]
        public async Task<int> AddTimetable([FromBody] AddTimeTableCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
