using CostaRicaMusic.BLL.Services;
using CostaRicaMusic.DAL.Context;
using CostaRicaMusic.DAL.Repositories;
using CostaRicaMusic.Web.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Servicios MVC
builder.Services.AddControllersWithViews();

// DbContext
builder.Services.AddDbContext<CostaRicaMusicContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<IArtistaRepository, ArtistaRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<ICancionRepository, CancionRepository>();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<IPlaylistCancionRepository, PlaylistCancionRepository>();

// Servicios BLL
builder.Services.AddScoped<IArtistaService, ArtistaService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<ICancionService, CancionService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddSession();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Pipeline
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();