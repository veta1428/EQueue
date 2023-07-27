using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Context;
using Rey.EQueue.Core.Entities;
using Rey.EQueue.Core.User;
using Rey.EQueue.EF.Configuration;

namespace Rey.EQueue.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly int? _groupId;

        public ApplicationDbContext(DbContextOptions options, IGroupContextAccessor groupAccessor)
            : base(options)
        {
            _groupId = groupAccessor.Current?.GroupId;
            Database.Migrate();
        }

        public DbSet<Class> Classes { get; set; } = null!;

        public DbSet<Queue> Queues { get; set; } = null!;

        public DbSet<Record> Records { get; set; } = null!;

        public DbSet<ScheduledClass> ScheduledClasses { get; set; } = null!;

        public DbSet<Subject> Subjects { get; set; } = null!;

        public DbSet<SubjectInstance> SubjectInstances { get; set; } = null!;

        public DbSet<SubjectInstanceTeacher> SubjectInstancesTeachers { get; set; } = null!;

        public DbSet<Teacher> Teachers { get; set; } = null!;

        public DbSet<Timetable> Timetables { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Group> Groups { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Class>(new ClassConfiguration());
            modelBuilder.ApplyConfiguration<Queue>(new QueueConfiguration());
            modelBuilder.ApplyConfiguration<Record>(new RecordConfiguration());
            modelBuilder.ApplyConfiguration<ScheduledClass>(new ScheduledClassConfiguration());
            modelBuilder.ApplyConfiguration<Subject>(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration<SubjectInstance>(new SubjectInstanceConfiguration());
            modelBuilder.ApplyConfiguration(new ChangeRequestConfiguration());

            modelBuilder.ApplyConfiguration<SubjectInstanceTeacher>(new SubjectInstanceTeacherConfiguration());
            modelBuilder.ApplyConfiguration<Teacher>(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration<Timetable>(new TimetableConfiguration());
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());

            if (_groupId is not null)
            {
                modelBuilder
                    .Entity<Teacher>()
                    .HasQueryFilter(t => t.GroupId == _groupId.Value);

                modelBuilder
                    .Entity<Subject>()
                    .HasQueryFilter(s => s.GroupId == _groupId.Value);
            }
        }
    }
}