using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, GetSubjectsQueryResult>
    {
        private readonly ISubjectRepository _subjectRepository;
        public GetSubjectsQueryHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<GetSubjectsQueryResult> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
        {
            var subjects = (await _subjectRepository.GetAsync(cancellationToken))
                .Select(s => new SubjectModel(s.Id, s.Name, s.Description));
            return new GetSubjectsQueryResult(subjects);
        }
    }
}
