using carritocompra.Infraestructure;
using carritocompra.Infraestructure.SeedWork;
using carritocompras.Application.Services;
using carritocompras.Application.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(
               builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("carritocompra")));

builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped(typeof(IRepository<>), typeof(Repository<>))    
    .AddScoped<IUserService, UsersService>()
    .AddScoped<IPersonaRepository, PersonaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Configura las políticas CORS aquí
app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

