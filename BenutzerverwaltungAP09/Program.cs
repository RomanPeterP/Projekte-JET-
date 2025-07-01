namespace BenutzerverwaltungAP09
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
                    // Nur für Zugriff von fremden Seiten notw.
                    builder.Services.AddCors(options =>
                    {
                        options.AddPolicy("AllowLocalhost777", policy =>
                        {
                            policy.WithOrigins("http://localhost:777")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                        });
                    });
                    var app = builder.Build();

                    app.UseCors("AllowLocalhost777");


            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllerRoute(
             name: "default",
             pattern: "{controller=BenutzerWeb}/{action=Registrieren}");
            app.Run();
        }
    }
}
