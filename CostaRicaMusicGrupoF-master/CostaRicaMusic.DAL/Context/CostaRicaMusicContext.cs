using System;
using System.Collections.Generic;
using System.Text;
using CostaRicaMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CostaRicaMusic.DAL.Context
{
    public class CostaRicaMusicContext : DbContext
    {
        public CostaRicaMusicContext(DbContextOptions<CostaRicaMusicContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Album> Albumes { get; set; }
        public DbSet<Cancion> Canciones { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistCancion> PlaylistCanciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlaylistCancion>()
                .HasKey(pc => new { pc.PlaylistId, pc.CancionId });

            modelBuilder.Entity<PlaylistCancion>()
                .HasOne(pc => pc.Playlist)
                .WithMany(p => p.PlaylistCanciones)
                .HasForeignKey(pc => pc.PlaylistId);

            modelBuilder.Entity<PlaylistCancion>()
                .HasOne(pc => pc.Cancion)
                .WithMany(c => c.PlaylistCanciones)
                .HasForeignKey(pc => pc.CancionId);

            modelBuilder.Entity<Album>()
                .HasOne(a => a.Artista)
                .WithMany(ar => ar.Albumes)
                .HasForeignKey(a => a.ArtistaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cancion>()
                .HasOne(c => c.Artista)
                .WithMany(a => a.Canciones)
                .HasForeignKey(c => c.ArtistaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cancion>()
                .HasOne(c => c.Album)
                .WithMany(a => a.Canciones)
                .HasForeignKey(c => c.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Playlists)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();

            modelBuilder.Entity<PlaylistCancion>()
                .HasIndex(pc => new { pc.PlaylistId, pc.CancionId })
                .IsUnique();

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Admin",
                    Correo = "admin@test.com",
                    PasswordHash = "1234",
                    FechaRegistro = new DateTime(2026, 3, 22)
                },
                new Usuario
                {
                    Id = 2,
                    Nombre = "Usuario Test",
                    Correo = "test@test.com",
                    PasswordHash = "1234",
                    FechaRegistro = new DateTime(2026, 3, 22)
                }
            );

            modelBuilder.Entity<Artista>().HasData(
                new Artista
                {
                    Id = 1,
                    Nombre = "Bad Bunny",
                    Pais = "Puerto Rico",
                    Descripcion = "Artista urbano y uno de los exponentes más conocidos del reggaetón y trap latino.",
                    ImagenUrl = "images/artists/badbunny.jpg"
                },
                new Artista
                {
                    Id = 2,
                    Nombre = "Adele",
                    Pais = "Reino Unido",
                    Descripcion = "Cantante británica reconocida por su potente voz y baladas pop.",
                    ImagenUrl = "images/artists/adele.jpg"
                },
                new Artista
                {
                    Id = 3,
                    Nombre = "Coldplay",
                    Pais = "Reino Unido",
                    Descripcion = "Banda británica de pop rock alternativo con reconocimiento internacional.",
                    ImagenUrl = "images/artists/coldplay.jpg"
                },
                new Artista
                {
                    Id = 4,
                    Nombre = "Karol G",
                    Pais = "Colombia",
                    Descripcion = "Cantante colombiana destacada en el género urbano y pop latino.",
                    ImagenUrl = "images/artists/karolg.jpg"
                },
                new Artista
                {
                    Id = 5,
                    Nombre = "Bruno Mars",
                    Pais = "Estados Unidos",
                    Descripcion = "Cantante y compositor estadounidense de pop, funk y R&B.",
                    ImagenUrl = "images/artists/brunomars.jpg"
                }
            );

            modelBuilder.Entity<Album>().HasData(
                new Album
                {
                    Id = 1,
                    Titulo = "Un Verano Sin Ti",
                    ArtistaId = 1,
                    Anio = 2022,
                    PortadaUrl = "images/albums/un-verano-sin-ti.jpg"
                },
                new Album
                {
                    Id = 2,
                    Titulo = "25",
                    ArtistaId = 2,
                    Anio = 2015,
                    PortadaUrl = "images/albums/25.jpg"
                },
                new Album
                {
                    Id = 3,
                    Titulo = "A Head Full of Dreams",
                    ArtistaId = 3,
                    Anio = 2015,
                    PortadaUrl = "images/albums/a-head-full-of-dreams.jpg"
                },
                new Album
                {
                    Id = 4,
                    Titulo = "Mañana Será Bonito",
                    ArtistaId = 4,
                    Anio = 2023,
                    PortadaUrl = "images/albums/manana-sera-bonito.jpg"
                },
                new Album
                {
                    Id = 5,
                    Titulo = "24K Magic",
                    ArtistaId = 5,
                    Anio = 2016,
                    PortadaUrl = "images/albums/24k-magic.jpg"
                }
            );

            modelBuilder.Entity<Cancion>().HasData(
                new Cancion
                {
                    Id = 1,
                    Nombre = "Tití Me Preguntó",
                    AlbumId = 1,
                    ArtistaId = 1,
                    Duracion = 240,
                    AudioUrl = "audio/titi-me-pregunto.mp3"
                },
                new Cancion
                {
                    Id = 2,
                    Nombre = "Ojitos Lindos",
                    AlbumId = 1,
                    ArtistaId = 1,
                    Duracion = 258,
                    AudioUrl = "audio/ojitos-lindos.mp3"
                },
                new Cancion
                {
                    Id = 3,
                    Nombre = "Hello",
                    AlbumId = 2,
                    ArtistaId = 2,
                    Duracion = 295,
                    AudioUrl = "audio/hello.mp3"
                },
                new Cancion
                {
                    Id = 4,
                    Nombre = "Send My Love",
                    AlbumId = 2,
                    ArtistaId = 2,
                    Duracion = 223,
                    AudioUrl = "audio/send-my-love.mp3"
                },
                new Cancion
                {
                    Id = 5,
                    Nombre = "Hymn for the Weekend",
                    AlbumId = 3,
                    ArtistaId = 3,
                    Duracion = 258,
                    AudioUrl = "audio/hymn-for-the-weekend.mp3"
                },
                new Cancion
                {
                    Id = 6,
                    Nombre = "Adventure of a Lifetime",
                    AlbumId = 3,
                    ArtistaId = 3,
                    Duracion = 263,
                    AudioUrl = "audio/adventure-of-a-lifetime.mp3"
                },
                new Cancion
                {
                    Id = 7,
                    Nombre = "Provenza",
                    AlbumId = 4,
                    ArtistaId = 4,
                    Duracion = 210,
                    AudioUrl = "audio/provenza.mp3"
                },
                new Cancion
                {
                    Id = 8,
                    Nombre = "Cairo",
                    AlbumId = 4,
                    ArtistaId = 4,
                    Duracion = 200,
                    AudioUrl = "audio/cairo.mp3"
                },
                new Cancion
                {
                    Id = 9,
                    Nombre = "24K Magic",
                    AlbumId = 5,
                    ArtistaId = 5,
                    Duracion = 227,
                    AudioUrl = "audio/24k-magic.mp3"
                },
                new Cancion
                {
                    Id = 10,
                    Nombre = "That's What I Like",
                    AlbumId = 5,
                    ArtistaId = 5,
                    Duracion = 206,
                    AudioUrl = "audio/thats-what-i-like.mp3"
                }
            );
        }
    }
}