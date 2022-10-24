using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Core.User;
using Rey.EQueue.EF;
using Rey.EQueue.Web.Extensions;

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

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddAuthentication();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

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

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller}/{action=Index}/{id?}");

            //app.MapRazorPages();

            //app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}