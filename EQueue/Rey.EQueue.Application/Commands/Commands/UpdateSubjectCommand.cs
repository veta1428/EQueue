using MediatR;
using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class UpdateSubjectCommand : IRequest
    {
        public UpdateSubjectCommand(SubjectModel subjectModel) 
        {
            SubjectModel = subjectModel;
        }

        public SubjectModel SubjectModel { get; set; }
    }
}
