﻿using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;
using Rey.EQueue.Shared.Exceptions;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddTimeTableCommandHandler : IRequestHandler<AddTimeTableCommand, int>
    {
        private readonly ITimetableRepository _timetableRepository;
        private readonly ISubjectInstanceRepository _subjectInstanceRepository;

        public AddTimeTableCommandHandler(
            ITimetableRepository timetableRepository, 
            ISubjectInstanceRepository subjectInstanceRepository)
        {
            _timetableRepository = timetableRepository;
            _subjectInstanceRepository = subjectInstanceRepository;
        }

        public async Task<int> Handle(AddTimeTableCommand request, CancellationToken cancellationToken)
        {
            var si = await _subjectInstanceRepository.FindByIdAsync(request.SubjectInstanceId, cancellationToken)
                ?? throw new NotFoundException(nameof(SubjectInstance));

            var timetable = new Timetable(request.AppliedPeriodStart, request.AppliedPeriodEnd, request.Classes.Select(c => new Class(c.DayOfWeek, c.StartTime, c.Duration)));
            _timetableRepository.Add(timetable);
            await _timetableRepository.SaveChangesAsync(cancellationToken);

            si.Timetables.Add(timetable);

            _subjectInstanceRepository.Update(si);
            await _subjectInstanceRepository.SaveChangesAsync(cancellationToken);
            return timetable.Id;
        }
    }
}