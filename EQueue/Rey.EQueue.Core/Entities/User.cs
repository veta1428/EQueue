using Rey.EQueue.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Core.Entities
{
    public class User : Entity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public ICollection<Record> Records { get; set; } = null!;
    }
}
