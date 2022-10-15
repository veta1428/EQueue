using Rey.EQueue.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Core.Entities
{
    public class Timetable : Entity
    {
        public DateTime AppliedPeriodStart { get; set; }

        public DateTime AppliedPeriodEnd { get; set; }

        public ICollection<Class>? Classes { get; set; }
    }
}
