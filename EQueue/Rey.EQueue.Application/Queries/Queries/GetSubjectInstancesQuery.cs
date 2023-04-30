using MediatR;
using Rey.EQueue.Application.Queries.QueryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Application.Queries.Queries
{
    public class GetSubjectInstancesQuery : IRequest<GetSubjectInstancesQueryResult>
    {
    }
}
