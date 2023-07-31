using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, GetTeachersQueryResult>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IRoleManager _roleManager;

        public GetTeachersQueryHandler(
            ITeacherRepository teacherRepository,
            IRoleManager roleManager)
        {
            _teacherRepository = teacherRepository;
            _roleManager = roleManager;
        }

        public async Task<GetTeachersQueryResult> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsUserInGroup())
                throw new InvalidOperationException("No access");

            var teachers = (await _teacherRepository.GetAsync(cancellationToken))
                .Select(t => new TeacherModel(t.Id, t.FirstName, t.LastName, t.MiddleName, t.Description, t.Note));
            return new GetTeachersQueryResult(teachers);
        }
    }
}
