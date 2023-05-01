using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class AddChangeRequestCommand : IRequest<int>
    {
        public AddChangeRequestCommand(int recordId, int queueId)
        {
            RecordId = recordId;
            QueueId = queueId;
        }

        public int RecordId { get; set; }

        public int QueueId { get; set; }
    }
}
