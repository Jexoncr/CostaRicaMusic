using System;
using System.Collections.Generic;
using System.Text;
using CostaRicaMusic.DAL.Context;
using CostaRicaMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CostaRicaMusic.DAL.Repositories
{
    public class PlaylistCancionRepository : IPlaylistCancionRepository
    {
        private readonly CostaRicaMusicContext _context;

        public PlaylistCancionRepository(CostaRicaMusicContext context)
        {
            _context = context;
        }

        public IEnumerable<PlaylistCancion> GetByPlaylistId(int playlistId)
        {
            return _context.PlaylistCanciones
                .Include(pc => pc.Cancion)
                    .ThenInclude(c => c!.Artista)
                .Include(pc => pc.Cancion)
                    .ThenInclude(c => c!.Album)
                .Where(pc => pc.PlaylistId == playlistId)
                .OrderBy(pc => pc.Orden)
                .ToList();
        }

        public PlaylistCancion? GetByIds(int playlistId, int cancionId)
        {
            return _context.PlaylistCanciones
                .FirstOrDefault(pc => pc.PlaylistId == playlistId && pc.CancionId == cancionId);
        }

        public bool Exists(int playlistId, int cancionId)
        {
            return _context.PlaylistCanciones
                .Any(pc => pc.PlaylistId == playlistId && pc.CancionId == cancionId);
        }

        public int GetNextOrder(int playlistId)
        {
            var ultimoOrden = _context.PlaylistCanciones
                .Where(pc => pc.PlaylistId == playlistId)
                .Select(pc => (int?)pc.Orden)
                .Max();

            return (ultimoOrden ?? 0) + 1;
        }

        public void Add(PlaylistCancion playlistCancion)
        {
            _context.PlaylistCanciones.Add(playlistCancion);
        }

        public void Delete(PlaylistCancion playlistCancion)
        {
            _context.PlaylistCanciones.Remove(playlistCancion);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}