using Microsoft.EntityFrameworkCore;
using Movie_Project.Data;
using Movie_Project.Models;

namespace Movie_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //// Add services to the container.
            
            builder.Services.AddDbContext<MovieDBContext>(Option =>
            {
                Option.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
            });
            builder.Services.AddSession(options =>
             {
                 options.IdleTimeout = TimeSpan.FromSeconds(31000);
                 options.Cookie.HttpOnly = true;
                 options.Cookie.IsEssential = true;
              });

            //// Add authentication services
            //builder.Services.AddAuthentication(options => {
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options => {
            //    options.Authority = "https://your-auth0-domain.auth0.com/";
            //    options.Audience = "your-api-audience";
            //});


            var app = builder.Build();
            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movie}/{action=Index}/{id?}");

            app.Run();
        }
    }
}