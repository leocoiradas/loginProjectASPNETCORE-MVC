using Microsoft.EntityFrameworkCore;
using loginProyectASPNETCORE_MVC.Models;
using loginProyectASPNETCORE_MVC.Services.Contract;
using loginProyectASPNETCORE_MVC.Services.Implementation;

namespace loginProyectASPNETCORE_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Configuraci�n del contexto de mi base de datos para tener acceso a ella desde la aplicaci�n
            //Nota: DbpruebaContext se refiere a la clase usada para conectar a nuestra base de datos de SQL server
            //Con este contexto, generamos la conexi�n a la db mediante la obtenci�n de la cadena de conexi�n que se esttableci� en
            //appsettings.json
            builder.Services.AddDbContext<DbpruebaContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLString"));
            });

            builder.Services.AddScoped<IUserService, UserService>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
