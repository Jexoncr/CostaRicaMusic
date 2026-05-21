using CostaRicaMusic.DAL.Context;
using CostaRicaMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CostaRicaMusic.DAL.Repositories
{
    public class ArtistaRepository : Repository<Artista>, IArtistaRepository
    {
        public ArtistaRepository(CostaRicaMusicContext context) : base(context)
        {
        }

        public IEnumerable<Artista> GetAllWithAlbums()
        {
            return _context.Artistas
                .Include(a => a.Albumes)
                .ToList();
        }

        public Artista? GetByIdWithSongs(int id)
        {
            return _context.Artistas
                .Include(a => a.Canciones)
                    .ThenInclude(c => c.Album)
                .FirstOrDefault(a => a.Id == id);
        }
        public IEnumerable<Artista> SearchByName(string? busqueda)
        {
            var query = _context.Artistas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                busqueda = busqueda.ToLower();
                query = query.Where(a => a.Nombre.ToLower().Contains(busqueda));
            }

            return query
                .OrderBy(a => a.Nombre)
                .ToList();
        }
    }
}