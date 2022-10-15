using Rey.EQueue.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Core.Entities
{
    public class Record : Entity
    {
        public Queue? Queue { get; set; }

        public int QueueId { get; set; }

        public int? PrevRecordId { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
    }
}
