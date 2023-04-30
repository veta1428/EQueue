using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand>
    {
        private readonly ISubjectRepository _subjectRepository;

        public UpdateSubjectCommandHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Unit> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.FindByIdAsync(request.SubjectModel.Id, cancellationToken);
            subject.Name = request.SubjectModel.Name;
            subject.Description = request.SubjectModel.Description;
            _subjectRepository.Update(subject);
            await _subjectRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
