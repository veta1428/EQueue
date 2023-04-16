using Rey.EQueue.Application.Queries.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Application.Queries.QueryResults
{
    public class GetSubjectInstancesQueryResult
    {
        public GetSubjectInstancesQueryResult(IEnumerable<SubjectInstanceModel> subjectInstances)
        {
            SubjectInstances = subjectInstances;
        }

        public IEnumerable<SubjectInstanceModel> SubjectInstances { get; set; }
    }
}
