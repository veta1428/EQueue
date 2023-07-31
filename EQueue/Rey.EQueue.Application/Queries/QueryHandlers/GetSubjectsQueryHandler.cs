using MediatR;
using Rey.EQueue.Application.Context;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
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
        private readonly IGroupContextAccessor _groupAccessor;
        private readonly IRoleManager _roleManager;
        public GetSubjectsQueryHandler(
            ISubjectRepository subjectRepository,
            IGroupContextAccessor groupAccessor,
            IRoleManager roleManager)
        {
            _subjectRepository = subjectRepository;
            _groupAccessor = groupAccessor;
            _roleManager = roleManager;
        }

        public async Task<GetSubjectsQueryResult> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsUserInGroup())
                throw new InvalidOperationException("No access");

            var subjects = (await _subjectRepository.GetAsync(cancellationToken))
                .Select(s => new SubjectModel(s.Id, s.Name, s.Description));
            return new GetSubjectsQueryResult(subjects);
        }
    }
}
