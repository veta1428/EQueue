using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddTeacherCommandHandler : IRequestHandler<AddTeacherCommand, int>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISubjectInstanceRepository _instanceRepository;

        public AddTeacherCommandHandler(
            ITeacherRepository teacherRepository, 
            ISubjectRepository subjectRepository, 
            ISubjectInstanceRepository instanceRepository)
        {
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
            _instanceRepository = instanceRepository;
        }

        public async Task<int> Handle(AddTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = new Teacher(
                request.FirstName, 
                request.LastName, 
                request.MiddleName, 
                request.Description, 
                request.Note);

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

