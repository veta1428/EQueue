using Rey.EQueue.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Core.Entities
{
    public class Teacher : Entity
    {
        public Teacher()
        {
        }

        public Teacher(
            string firstName,
            string lastName,
            string? middleName = null,
            string? description = null,
            string? note = null)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Description = description;
            Note = note;
        }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public string? Description { get; set; }

        public string? Note { get; set; }

        public ICollection<SubjectInstanceTeacher> SubjectInstanceTeachers { get; set; } = null!;
    }
}
