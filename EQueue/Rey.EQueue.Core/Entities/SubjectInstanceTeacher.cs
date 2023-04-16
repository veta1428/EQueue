using Rey.EQueue.Shared;

namespace Rey.EQueue.Core.Entities
{
    public class SubjectInstanceTeacher : Entity
    {
        public SubjectInstanceTeacher() { }
        public SubjectInstanceTeacher(
            Teacher teacher, 
            SubjectInstance subjectInstance)
        {
            SubjectInstance = subjectInstance;
            Teacher = teacher;
        }

        public SubjectInstanceTeacher(int subjectInstanceId, int teacherId)
        {
            SubjectInstanceId = subjectInstanceId;
            TeacherId = teacherId;
        }

        public int SubjectInstanceId { get; set; }
        
        public SubjectInstance? SubjectInstance { get; set; }

        public int TeacherId { get; set; }

        public Teacher? Teacher { get; set; }
    }
}
