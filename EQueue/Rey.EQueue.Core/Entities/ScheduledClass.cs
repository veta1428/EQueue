using Rey.EQueue.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Core.Entities
{
    public class ScheduledClass : Entity
    {
        public string? Description { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public int SubjectInstanceId { get; set; }

        public SubjectInstance? SubjectInstance { get; set; }

        public ICollection<Queue>? Queues { get; set; }
    }
}
