using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryResults;

namespace Rey.EQueue.Web.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectInstanceController : ControllerBase
    {
        private IMediator _mediator;

        public SubjectInstanceController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
