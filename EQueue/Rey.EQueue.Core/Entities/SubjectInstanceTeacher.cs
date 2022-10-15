using Rey.EQueue.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Core.Entities
{
    public class SubjectInstanceTeacher : Entity
    {
        public int SubjectInstanceId { get; set; }
        
        public SubjectInstance? SubjectInstance { get; set; }

        public int TeacherId { get; set; }

        public Teacher? Teacher { get; set; }

    }
}
