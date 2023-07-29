using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Core.User;
using Rey.EQueue.EF;
using Rey.EQueue.Web.Extensions;
using System.Reflection;
using Rey.EQueue.Web.Services;
using Rey.EQueue.Application.Services;
using System.Text.Json.Serialization;
using Rey.EQueue.Web.Middlewares;
using Rey.EQueue.Application.Context;
using Rey.EQueue.Web.Context;
using Rey.EQueue.Application.Options;
using Rey.EQueue.Application.Commands.Commands;

namespace Rey.EQueue.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddOptions<DeactivateQueueOptions>()
                .Bind(builder.Configuration.GetSection("DeactivateQueue"));


            builder.Services.AddOptions<GenerateQueueOptions>().Bind(builder.Configuration.GetSection("GenerateQueue"));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.ConfigureRepositories();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddAuthentication();
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<IUserAccessor, UserAccessor>();
            builder.Services.AddSingleton<IGroupContextScheduler, GroupContextScheduler>();
            builder.Services.AddSingleton<IGroupContextAccessor, GroupContextAccessor>();


            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(opts => {
                    opts.Password.RequiredLength = 3;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services
                .AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            //foreach (var assembly in domainAssemblies)
            //{
            //    Console.WriteLine(assembly.FullName);
            //}

            builder.Services.AddMediatR(domainAssemblies);

            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            var mediatr = scope.ServiceProvider.GetRequiredService<IMediator>();
            var scheduler = Rey.EQueue.Shared.TaskScheduler.Instance;
            scheduler.RunAndScheduleTask(7, async () 
                => await mediatr.Send(new SynchronizeCommand(), CancellationToken.None));

            //scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMiddleware<GroupContextMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.MapControllers();

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";

                //if (env.IsDevelopment())
                //{
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
                //}
            });

            app.MapRazorPages();

            //app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}