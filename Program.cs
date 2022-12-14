using TestTask.Interfaces;
using TestTask.Models;
using TestTask.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FolderContext>(options =>
                options.UseSqlServer(connection));
builder.Services.AddScoped<IDbFolder, DbFolder>();
builder.Services.AddScoped<IDataProvider, DataProvider>();
builder.Services.AddTransient<IFoldersManager, FoldersManger>();

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
    pattern: "{controller=Home}/{action=Index}/{path?}");

app.Run();
