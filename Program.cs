using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SistemaRestaurante.Data; // ou o namespace onde está seu AppDbContext

namespace SistemaRestaurante
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1?? Adiciona o middleware de autenticação por cookie
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Login";          // rota de login
                    options.LogoutPath = "/Auth/Logout";        // rota de logout
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                });

            // ?? Configuração do banco de dados MySQL com Pomelo
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 36)) // ajuste conforme a versão do seu MySQL
                )
            );

            // MVC
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(); // Isso carrega os arquivos estáticos (CSS, JS etc.)

            app.UseRouting();

            app.UseAuthentication();  // <— antes do UseAuthorization
            app.UseAuthorization();


            app.MapControllerRoute(
             name: "default",
             pattern: "{controller=Auth}/{action=Login}/{id?}");
            app.Run();
        }
    }
}
