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
        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public string? Description { get; set; }

        public string? Note { get; set; }

        public ICollection<SubjectInstanceTeacher> SubjectInstanceTeachers { get; set; } = null!;
    }
}
