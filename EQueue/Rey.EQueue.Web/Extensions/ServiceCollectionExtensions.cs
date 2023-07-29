using Rey.EQueue.Application.Repositories;
using Rey.EQueue.EF.Repositories;

namespace Rey.EQueue.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IQueueRepository, QueueRepository>();
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<IScheduledClassRepository, ScheduledClassRepository>();
            services.AddScoped<ISubjectInstanceRepository, SubjectInstanceRepository>();
            services.AddScoped<ISubjectInstanceTeacherRepository, SubjectInstanceTeacherRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITimetableRepository, TimetableRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChangeRequestRepository, ChangeRequestRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
        }
    }
}
