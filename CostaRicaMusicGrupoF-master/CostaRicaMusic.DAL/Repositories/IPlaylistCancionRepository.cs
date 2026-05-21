using System;
using System.Collections.Generic;
using System.Text;
using CostaRicaMusic.DAL.Entities;
using System.Collections.Generic;

namespace CostaRicaMusic.DAL.Repositories
{
    public interface IPlaylistCancionRepository
    {
        IEnumerable<PlaylistCancion> GetByPlaylistId(int playlistId);
        PlaylistCancion? GetByIds(int playlistId, int cancionId);
        bool Exists(int playlistId, int cancionId);
        int GetNextOrder(int playlistId);
        void Add(PlaylistCancion playlistCancion);
        void Delete(PlaylistCancion playlistCancion);
        void Save();
    }
}