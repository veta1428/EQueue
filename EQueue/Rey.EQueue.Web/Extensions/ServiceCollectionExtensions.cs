namespace Rey.EQueue.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<>();
        }
    }
}
