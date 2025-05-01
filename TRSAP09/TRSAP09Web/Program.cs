using TRSAP09.Logic;
using TRSAP09.Models.Interfaces;

namespace TRSAP09Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddScoped<IRestaurantLogic, RestaurantLogic>();
            builder.Services.AddScoped<IRestaurantMapper, RestaurantMapper>();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Restaurant}/{action=List}/{id?}");

            app.Run();
        }
    }
}
