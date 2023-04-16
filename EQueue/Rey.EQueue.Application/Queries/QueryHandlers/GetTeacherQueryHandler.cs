using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Repositories;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, TeacherModel>
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetTeacherQueryHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<TeacherModel> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {
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
