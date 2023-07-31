using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Context;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddTeacherCommandHandler : IRequestHandler<AddTeacherCommand, int>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISubjectInstanceRepository _instanceRepository;
        private readonly IGroupContextAccessor _groupContextAccessor;
        private readonly IRoleManager _roleManager;

        public AddTeacherCommandHandler(
            ITeacherRepository teacherRepository, 
            ISubjectRepository subjectRepository, 
            ISubjectInstanceRepository instanceRepository,
            IGroupContextAccessor groupContextAccessor,
            IRoleManager roleManager)
        {
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
            _instanceRepository = instanceRepository;
            _groupContextAccessor = groupContextAccessor;
            _roleManager = roleManager;
        }

        public async Task<int> Handle(AddTeacherCommand request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsAdminInGroup())
                throw new InvalidOperationException("No access");

            var groupId = _groupContextAccessor.Current?.GroupId ?? throw new InvalidOperationException($"{nameof(Teacher.GroupId)} is absent");

            var teacher = new Teacher(
                request.FirstName,
                request.LastName,
                request.MiddleName,
                request.Description,
                request.Note)
            {  GroupId = groupId };

            if (request.SubjectInstanceIds is not null)
            {
                var subjectsInstances = await _instanceRepository.GetByIdsAsync(request.SubjectInstanceIds, cancellationToken);

                foreach (var subjectInstance in subjectsInstances)
                {
                    teacher.SubjectInstanceTeachers.Add(new SubjectInstanceTeacher() { SubjectInstance = subjectInstance, Teacher = teacher });
                }
            }

            _teacherRepository.Add(teacher);

            await _teacherRepository.SaveChangesAsync(cancellationToken);
            return teacher.Id;
        }
    }
}

