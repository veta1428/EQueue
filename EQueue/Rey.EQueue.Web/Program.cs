using MediatR;
using Microsoft.EntityFrameworkCore;
using Rey.EQueue.EF;
using Rey.EQueue.Web.Extensions;
using System.Reflection;

namespace Rey.EQueue.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.ConfigureRepositories();
            Console.WriteLine(Assembly.GetEntryAssembly().ToString());

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddAuthentication();

            builder.Services.AddControllers();

            var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in domainAssemblies)
            {
                Console.WriteLine(assembly.FullName);
            }

            builder.Services.AddMediatR(domainAssemblies);

            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


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
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use((co, to) =>
            {
                int a = 9;
                return to(co);
            });

            app.MapControllers();
            //app.UseSpa(spa =>
            //{
            //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    // see https://go.microsoft.com/fwlink/?linkid=864501
            //    spa.Options.SourcePath = "PracticeAppClient";

            //    if (Environment.IsLocal())
            //    {
            //        // spa.UseAngularCliServer(npmScript: "start");
            //        spa.UseProxyToSpaDevelopmentServer("http://localhost:4203");
            //    }
            //});
            //app.MapRazorPages();

            //app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}