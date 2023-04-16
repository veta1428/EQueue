﻿using Rey.EQueue.Core.Entities;
using Rey.EQueue.Shared.Interfaces;

namespace Rey.EQueue.Application.Repositories
{
    public interface ISubjectInstanceRepository: IRepository<SubjectInstance, int>
    {
        Task<IEnumerable<SubjectInstance>> GetBySubjectIdAsync(int sibjectId, CancellationToken cancellationToken);
    }
}