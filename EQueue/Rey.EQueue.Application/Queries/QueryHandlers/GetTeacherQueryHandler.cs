using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, TeacherModel>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IRoleManager _roleManager;

        public GetTeacherQueryHandler(
            ITeacherRepository teacherRepository,
            IRoleManager roleManager)
        {
            _teacherRepository = teacherRepository;
            _roleManager = roleManager;
        }

        public async Task<TeacherModel> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsUserInGroup())
                throw new InvalidOperationException("No access");

            var teacher = await _teacherRepository.FindByIdAsync(request.Id, cancellationToken);

            return new TeacherModel(
                teacher.Id, 
                teacher.FirstName, 
                teacher.LastName, 
                teacher.MiddleName, 
                teacher.Description, 
                teacher.Note);
        }
    }
}
