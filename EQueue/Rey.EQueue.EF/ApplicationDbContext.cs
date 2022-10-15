using Duende.IdentityServer.EntityFramework.Options;
using Rey.EQueue.Core.User;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<>();
        }
    }
}