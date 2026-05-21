using CostaRicaMusic.DAL.Context;
using CostaRicaMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CostaRicaMusic.DAL.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(CostaRicaMusicContext context) : base(context)
        {
        }

        public IEnumerable<Album> GetAllWithArtista()
        {
            return _context.Albumes
                .Include(a => a.Artista)
                .ToList();
        }

        public IEnumerable<Album> GetByArtistaId(int artistaId)
        {
            return _context.Albumes
                .Include(a => a.Artista)
                .Where(a => a.ArtistaId == artistaId)
                .ToList();
        }

        public IEnumerable<Album> SearchByName(string? busqueda)
        {
            var query = _context.Albumes.Include(a => a.Artista).AsQueryable();

            if (!string.IsNullOrWhiteSpace(busqueda))
                query = query.Where(a =>
                    a.Titulo.ToLower().Contains(busqueda.ToLower()) ||
                    (a.Artista != null && a.Artista.Nombre.ToLower().Contains(busqueda.ToLower())));

            return query.OrderBy(a => a.Titulo).ToList();
        }

        public Album? GetByIdWithSongs(int id)
        {
            return _context.Albumes
                .Include(a => a.Artista)
                .Include(a => a.Canciones)
                .FirstOrDefault(a => a.Id == id);
        }
    }
}