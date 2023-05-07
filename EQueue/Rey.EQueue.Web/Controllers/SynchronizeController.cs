using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rey.EQueue.Application.Commands.Commands;

namespace Rey.EQueue.Web.Controllers
{
    [Route("synchronize")]
    public class SynchronizeController : Controller
    {
        private readonly IMediator _mediator;

        public SynchronizeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            await _mediator.Send(new SynchronizeCommand(), cancellationToken);
            return new OkObjectResult("Done");
        }
    }
}
