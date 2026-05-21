using System;
using System.Collections.Generic;
using System.Text;
using CostaRicaMusic.DAL.Entities;
using System.Collections.Generic;

namespace CostaRicaMusic.DAL.Repositories
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        IEnumerable<Playlist> GetByUsuarioId(int usuarioId);
        Playlist? GetByIdWithSongs(int id);
    }
}