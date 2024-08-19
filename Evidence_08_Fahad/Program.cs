using Evidence_08_Fahad.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//ConfigureServces
builder.Services.AddDbContext<ProductDbContext>(
    op => op.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddControllersWithViews();
var app = builder.Build();
//Configure
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
