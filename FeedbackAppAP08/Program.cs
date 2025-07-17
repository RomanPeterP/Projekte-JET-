using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var sqlServerInstanceName = Environment.GetEnvironmentVariable("SqlServerInstanceName", EnvironmentVariableTarget.User);
builder.Services.AddDbContext<FeedbackDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    .Replace("@SqlServerInstanceName", sqlServerInstanceName)));

builder.Services.AddMemoryCache();
builder.Services.AddAuthentication("FeedbackCookieAuth")
    .AddCookie("FeedbackCookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Session Informationen werden im MemoryCache gehalten
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
             name: "default",
             pattern: "{controller=Feedback}/{action=Index}");
app.Run();