using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Queries.QueryModels
{
    public class TimetableModel
    {
        public TimetableModel(DateTime appliedPeriodStart, DateTime appliedPeriodEnd, IEnumerable<ClassModel>? classes)
        {
            AppliedPeriodStart = appliedPeriodStart;
            AppliedPeriodEnd = appliedPeriodEnd;
            Classes = classes;
        }

        public DateTime AppliedPeriodStart { get; set; }

        public DateTime AppliedPeriodEnd { get; set; }

        public IEnumerable<ClassModel>? Classes { get; set; }
    }
}
