using CostaRicaMusic.BLL.Services;
using CostaRicaMusic.DAL.Context;
using CostaRicaMusic.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Servicios de controladores + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<CostaRicaMusicContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<IArtistaRepository, ArtistaRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<ICancionRepository, CancionRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Servicios BLL
builder.Services.AddScoped<IArtistaService, ArtistaService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<ICancionService, CancionService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();