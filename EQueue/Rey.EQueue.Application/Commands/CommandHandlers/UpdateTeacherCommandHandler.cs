using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand>
    {
        private readonly ITeacherRepository _teacherRepository;

        public UpdateTeacherCommandHandler(ITeacherRepository teacherRepository) 
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Unit> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.FindByIdAsync(request.TeacherModel.Id, cancellationToken);
            teacher.FirstName = request.TeacherModel.FirstName;
            teacher.LastName = request.TeacherModel.LastName;
            teacher.MiddleName = request.TeacherModel.MiddleName;
            teacher.Description = request.TeacherModel.Description;
            teacher.Note = request.TeacherModel.Note;

            _teacherRepository.Update(teacher);
            await _teacherRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
