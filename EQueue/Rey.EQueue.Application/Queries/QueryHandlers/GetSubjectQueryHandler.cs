using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, SubjectModel>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IRoleManager _roleManager;

        public GetSubjectQueryHandler(
            ISubjectRepository subjectRepository,
            IRoleManager roleManager)
        {
            _subjectRepository = subjectRepository;
            _roleManager = roleManager;
        }

        public async Task<SubjectModel> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsUserInGroup())
                throw new InvalidOperationException("No access");

            var subject = await _subjectRepository.FindByIdAsync(request.SubjectId, cancellationToken);
            return new SubjectModel(subject.Id, subject.Name, subject.Description);
        }
    }
}
