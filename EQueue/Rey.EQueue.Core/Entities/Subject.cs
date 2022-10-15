using Rey.EQueue.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Core.Entities
{
    public class Subject : Entity
    {
        public Subject(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<SubjectInstance> SubjectInstances { get; set; }

    }
}
