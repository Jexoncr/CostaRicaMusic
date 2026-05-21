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
    public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(CostaRicaMusicContext context) : base(context)
        {
        }

        public IEnumerable<Playlist> GetByUsuarioId(int usuarioId)
        {
            return _context.Playlists
                .Include(p => p.PlaylistCanciones)
                    .ThenInclude(pc => pc.Cancion)
                        .ThenInclude(c => c.Album)
                .Include(p => p.PlaylistCanciones)
                    .ThenInclude(pc => pc.Cancion)
                        .ThenInclude(c => c!.Artista)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        public Playlist? GetByIdWithSongs(int id)
        {
            return _context.Playlists
                .Include(p => p.PlaylistCanciones)
                    .ThenInclude(pc => pc.Cancion)
                        .ThenInclude(c => c!.Artista)
                .Include(p => p.PlaylistCanciones)
                    .ThenInclude(pc => pc.Cancion)
                        .ThenInclude(c => c!.Album)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}