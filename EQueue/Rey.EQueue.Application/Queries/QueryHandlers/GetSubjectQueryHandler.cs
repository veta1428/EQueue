using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Repositories;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, SubjectModel>
    {
        private readonly ISubjectRepository _subjectRepository;

        public GetSubjectQueryHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<SubjectModel> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.FindByIdAsync(request.SubjectId, cancellationToken);
            return new SubjectModel(subject.Id, subject.Name, subject.Description);
        }
    }
}
