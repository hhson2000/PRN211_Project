using Microsoft.EntityFrameworkCore;
using ProjectPrn211.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CenimaDBContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Cenima"));
});
builder.Services.AddSession();

var app = builder.Build();

app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
