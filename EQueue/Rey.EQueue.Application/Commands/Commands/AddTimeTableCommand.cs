using MediatR;
using Rey.EQueue.Application.Commands.Models;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class AddTimeTableCommand : IRequest<int>
    {
        public int SubjectInstanceId { get; set; }

        public DateTime AppliedPeriodStart { get; set; }

        public DateTime AppliedPeriodEnd { get; set; }

        public IEnumerable<ClassModel> Classes { get; set; } = null!;
    }
}
