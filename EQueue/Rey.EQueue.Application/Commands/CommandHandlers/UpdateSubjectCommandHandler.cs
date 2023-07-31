using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IRoleManager _roleManager;

        public UpdateSubjectCommandHandler(
            ISubjectRepository subjectRepository,
            IRoleManager roleManager)
        {
            _subjectRepository = subjectRepository;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsAdminInGroup())
                throw new InvalidOperationException("No access");

            var subject = await _subjectRepository.FindByIdAsync(request.SubjectModel.Id, cancellationToken);
            subject.Name = request.SubjectModel.Name;
            subject.Description = request.SubjectModel.Description;
            _subjectRepository.Update(subject);
            await _subjectRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
