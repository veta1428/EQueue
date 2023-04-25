using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddSubjectInstanceCommandHandler : IRequestHandler<AddSubjectInstanceCommand, int>
    {
        private readonly ISubjectInstanceRepository _subjectInstanceRepository;
        private readonly ITeacherRepository _teacherRepository;

        public AddSubjectInstanceCommandHandler(ISubjectInstanceRepository subjectInstanceRepository, ITeacherRepository teacherRepository)
        {
            _subjectInstanceRepository = subjectInstanceRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<int> Handle(AddSubjectInstanceCommand request, CancellationToken cancellationToken)
        {

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
