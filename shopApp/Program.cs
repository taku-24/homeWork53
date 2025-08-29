using Microsoft.EntityFrameworkCore;
using shopApp.Models;
using Westwind.AspNetCore.LiveReload;

namespace shopApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Подключаем LiveReload
        builder.Services.AddLiveReload();

        // Добавляем MVC + Razor Runtime Compilation
        builder.Services.AddControllersWithViews()
            .AddRazorRuntimeCompilation();

        // Подключаем БД
        string connection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ShopContext>(options => options.UseSqlite(connection));

        var app = builder.Build();

        // Включаем LiveReload
        app.UseLiveReload();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Products}/{action=Index}/{id?}");

        app.Run();
    }
}