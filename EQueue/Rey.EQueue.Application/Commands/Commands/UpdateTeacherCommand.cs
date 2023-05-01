using MediatR;
using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class UpdateTeacherCommand : IRequest
    {
        public UpdateTeacherCommand(TeacherModel teacher) 
        { 
            TeacherModel = teacher;
        }

        public TeacherModel TeacherModel { get; set; }
    }
}
