using CostaRicaMusic.DAL.Context;
using CostaRicaMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CostaRicaMusic.DAL.Repositories
{
    public class CancionRepository : Repository<Cancion>, ICancionRepository
    {
        public CancionRepository(CostaRicaMusicContext context) : base(context)
        {
        }
        public IEnumerable<Cancion> GetAll()
        {
            return _context.Canciones
                .Include(c => c.Artista)
                .Include(c => c.Album)
                .OrderBy(c => c.Nombre)
                .ToList();
        }

        public IEnumerable<Cancion> GetAllWithRelations()
        {
            return _context.Canciones
                .Include(c => c.Artista)
                .Include(c => c.Album)
                .OrderBy(c => c.Nombre)
                .ToList();
        }

        public IEnumerable<Cancion> SearchByName(string busqueda)
        {
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                return _context.Canciones
                    .Include(c => c.Artista)
                    .Include(c => c.Album)
                    .OrderBy(c => c.Nombre)
                    .ToList();
            }

            busqueda = busqueda.ToLower();

            return _context.Canciones
                .Include(c => c.Artista)
                .Include(c => c.Album)
                .Where(c =>
                    c.Nombre.ToLower().Contains(busqueda) ||
                    (c.Artista != null && c.Artista.Nombre.ToLower().Contains(busqueda)) ||
                    (c.Album != null && c.Album.Titulo.ToLower().Contains(busqueda)))
                .OrderBy(c => c.Nombre)
                .ToList();
        }

        public IEnumerable<Cancion> GetPaged(int pageNumber, int pageSize)
        {
            return _context.Canciones
                .Include(c => c.Artista)
                .Include(c => c.Album)
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}