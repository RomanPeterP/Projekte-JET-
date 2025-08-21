using CustomLogger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TableReservationSystem.Data;
using TableReservationSystem.Logic;
using TableReservationSystem.Models;
using TableReservationSystem.Models.Interfaces;

namespace TableReservationSystemWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            var sqlServerInstanceName = Environment.GetEnvironmentVariable("SqlServerInstanceName", EnvironmentVariableTarget.User);
            builder.Services.AddScoped<IRestaurantLogic, RestaurantLogic>();
            builder.Services.AddScoped<IMiscLogic, MiscLogic>();
            builder.Services.AddScoped<IRestaurantMapper, RestaurantMapper>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddDbContext<TableReservationSystemContext>(options =>
                options.UseSqlServer(Config.ConfigItems.GetConnectionString("default")
                .Replace("@SqlServerInstanceName", sqlServerInstanceName)));
                  
            builder.Services.AddScoped<TableReservationSystemContext, TableReservationSystemContext>();
            builder.Services.AddScoped<IRestaurant, Restaurant>();
            builder.Services.AddScoped<IResponse<IRestaurant>, Response<IRestaurant>>();
            builder.Services.AddScoped<IResponse<Country>, Response<Country>>();
            builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            builder.Services.AddScoped<IMiscRepository, MiscRepository>();
            builder.Services.AddAuthentication("TRSCookieAuth")
            .AddCookie("TRSCookieAuth", options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            builder.ConfigureLogger();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");
            app.Run();
        }
    }
}
