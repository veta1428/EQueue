using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Context;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddSubjectCommandHandler
        : IRequestHandler<AddSubjectCommand, int>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IGroupContextAccessor _groupContextAccessor;
        private readonly IRoleManager _roleManager;

        public AddSubjectCommandHandler(
            ISubjectRepository subjectRepository, 
            IGroupContextAccessor groupContextAccessor,
            IRoleManager roleManager)
        {
            _subjectRepository = subjectRepository;
            _groupContextAccessor = groupContextAccessor;
            _roleManager = roleManager;
        }

        public async Task<int> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsAdminInGroup())
                throw new InvalidOperationException("No access");

            int groupId = _groupContextAccessor.Current?.GroupId ?? throw new InvalidOperationException($"{nameof(Subject.GroupId)} is absent");

            // ToDo: check access
            var subject = new Subject(request.Name, request.Description) { GroupId = groupId };
            _subjectRepository.Add(subject);
            await _subjectRepository.SaveChangesAsync(cancellationToken);
            return subject.Id;
        }
    }
}
