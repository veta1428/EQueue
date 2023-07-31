using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddSubjectInstanceCommandHandler : IRequestHandler<AddSubjectInstanceCommand, int>
    {
        private readonly ISubjectInstanceRepository _subjectInstanceRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IRoleManager _roleManager;

        public AddSubjectInstanceCommandHandler(
            ISubjectInstanceRepository subjectInstanceRepository,
            ITeacherRepository teacherRepository,
            IRoleManager roleManager)
        {
            _subjectInstanceRepository = subjectInstanceRepository;
            _teacherRepository = teacherRepository;
            _roleManager = roleManager;
        }

        public async Task<int> Handle(AddSubjectInstanceCommand request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsAdminInGroup())
                throw new InvalidOperationException("No access");

            var si = new SubjectInstance() { Name = request.Name, Description = request.Description, SubjectId = request.SubjectId };

            var teachers = new List<Teacher>();

            foreach (var id in request.TeacherIds)
            {
                var teacher = await _teacherRepository.FindByIdAsync(id, cancellationToken);
                teachers.Add(teacher);
            }

            foreach (var teacher in teachers)
            {
                si.SubjectInstanceTeachers.Add(new SubjectInstanceTeacher(teacher, si));
            }

            _subjectInstanceRepository.Add(si);

            await _subjectInstanceRepository.SaveChangesAsync(cancellationToken);
            return si.Id;
        }
    }
}
