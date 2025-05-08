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
            builder.Services.AddScoped<IRestaurantLogic, RestaurantLogic>();
            builder.Services.AddScoped<IRestaurantMapper, RestaurantMapper>();
            builder.Services.AddDbContext<TableReservationSystemContext>(options =>
                options.UseSqlServer(Config.ConfigItems.GetConnectionString("default")));
            builder.Services.AddScoped<TableReservationSystemContext, TableReservationSystemContext>();
            builder.Services.AddScoped<IRestaurant, Restaurant>();
            builder.Services.AddScoped<IResponse, Response>();
            builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Restaurant}/{action=Register}/{id?}");

            app.Run();
        }
    }
}
