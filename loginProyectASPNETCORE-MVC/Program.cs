using Microsoft.EntityFrameworkCore;
using loginProyectASPNETCORE_MVC.Models;
using loginProyectASPNETCORE_MVC.Services.Contract;
using loginProyectASPNETCORE_MVC.Services.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace loginProyectASPNETCORE_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Configuración del contexto de mi base de datos para tener acceso a ella desde la aplicación
            //Nota: DbpruebaContext se refiere a la clase usada para conectar a nuestra base de datos de SQL server
            //Con este contexto, generamos la conexión a la db mediante la obtención de la cadena de conexión que se esttableció en
            //appsettings.json
            builder.Services.AddDbContext<DbpruebaContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLString"));
            });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(300);
                });

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(
                    new ResponseCacheAttribute
                    {
                        NoStore = true,
                        Location = ResponseCacheLocation.None,
                    });
            });

            var app = builder.Build();

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
