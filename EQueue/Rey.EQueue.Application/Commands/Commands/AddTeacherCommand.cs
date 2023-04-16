using MediatR;
using Rey.EQueue.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class AddTeacherCommand : IRequest<int>
    {
        public AddTeacherCommand(
            string firstName, 
            string lastName, 
            string? middleName = null, 
            string? description = null, 
            string? note = null,
            IEnumerable<int>? subjectInstanceIds = null)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Description = description;
            Note = note;
            SubjectInstanceIds = subjectInstanceIds;
        }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public string? Description { get; set; }

        public string? Note { get; set; }

        public IEnumerable<int>? SubjectInstanceIds { get; set; }
    }
}
