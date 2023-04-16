﻿using Rey.EQueue.Core.Entities;
using Rey.EQueue.Shared.Interfaces;

namespace Rey.EQueue.Application.Repositories
{
    public interface ISubjectInstanceTeacherRepository: IRepository<SubjectInstanceTeacher, int>
    {
        Task<IEnumerable<SubjectInstanceTeacher>> GetByTeacherAsync(int teacherId, CancellationToken cancellationToken);
    }
}
