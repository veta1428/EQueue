using Rey.EQueue.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Core.Entities
{
    public class Queue : Entity
    {
        public DateTime CreationDate { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public int ScheduledClassId { get; set; }

        public int GroupId { get; set; }

        public ScheduledClass? ScheduledClass { get; set; }

        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}
