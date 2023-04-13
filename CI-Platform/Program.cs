using CI_Entity.Models;
using CI_PlatForm.Repository.Interface;
using CI_PlatForm.Repository.Repository;
using Microsoft.CodeAnalysis.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CIDbContext>();
builder.Services.AddScoped<IUserInterface,UserRepository>();

builder.Services.AddSession(Option => { 

Option.IdleTimeout=TimeSpan.FromHours(10);
});

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

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Landingpage}/{action=Landingpage}/{id?}");
   // pattern: "{area=Admin}/{controller=Admin}/{action=Admin}/{id?}");

app.Run();
