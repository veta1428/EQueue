using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Context;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddSubjectCommandHandler
        : IRequestHandler<AddSubjectCommand, int>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IGroupContextAccessor _groupContextAccessor;

        public AddSubjectCommandHandler(
            ISubjectRepository subjectRepository, 
            IGroupContextAccessor groupContextAccessor)
        {
            _subjectRepository = subjectRepository;
            _groupContextAccessor = groupContextAccessor;
        }

        public async Task<int> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            int groupId = _groupContextAccessor.Current?.GroupId ?? throw new InvalidOperationException($"{nameof(Subject.GroupId)} is absent");

            // ToDo: check access
            var subject = new Subject(request.Name, request.Description) { GroupId = groupId };
            _subjectRepository.Add(subject);
            await _subjectRepository.SaveChangesAsync(cancellationToken);
            return subject.Id;
        }
    }
}
