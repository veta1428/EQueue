using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddSubjectCommandHandler
        : IRequestHandler<AddSubjectCommand, int>
    {
        private readonly ISubjectRepository _subjectRepository;
        public AddSubjectCommandHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<int> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = new Subject(request.Name, request.Description);
            _subjectRepository.Add(subject);
            await _subjectRepository.SaveChangesAsync(cancellationToken);
            return subject.Id;
        }
    }
}
