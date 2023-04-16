using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, GetTeachersQueryResult>
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetTeachersQueryHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<GetTeachersQueryResult> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = (await _teacherRepository.GetAsync(cancellationToken))
                .Select(t => new TeacherModel(t.Id, t.FirstName, t.LastName, t.MiddleName, t.Description, t.Note));
            return new GetTeachersQueryResult(teachers);
        }
    }
}
