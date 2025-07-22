using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebApp.Data;
var builder = WebApplication.CreateBuilder(args);

ServerVersion serverVersion = ServerVersion.Create(
    MySqlServerVersion.LatestSupportedServerVersion.Version, 
    Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql);

builder.Services.AddDbContext<SalesWebAppContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("SalesWebAppContext") ?? throw new InvalidOperationException("Connection string 'SalesWebAppContext' not found."),
    serverVersion,
    builder => builder.MigrationsAssembly("SalesWebApp")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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
