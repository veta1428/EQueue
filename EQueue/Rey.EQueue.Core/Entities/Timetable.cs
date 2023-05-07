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
        public Timetable(){ }
        public Timetable(DateTime appliedPeriodStart, DateTime appliedPeriodEnd, IEnumerable<Class>? classes)
        {
            AppliedPeriodStart = appliedPeriodStart;
            AppliedPeriodEnd = appliedPeriodEnd;
            Classes = classes;
        }

        public DateTime AppliedPeriodStart { get; set; }

        public DateTime AppliedPeriodEnd { get; set; }

        public IEnumerable<Class>? Classes { get; set; }

        public bool IsActive { get; set; }
    }
}
