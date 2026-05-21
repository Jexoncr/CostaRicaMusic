using System;
using System.Collections.Generic;
using System.Text;
using CostaRicaMusic.DAL.Entities;
using System.Collections.Generic;

namespace CostaRicaMusic.BLL.Services
{
    public interface IPlaylistService
    {
        IEnumerable<Playlist> GetByUsuarioId(int usuarioId);
        Playlist? GetById(int id);
        Playlist? GetByIdWithSongs(int id);
        void Add(Playlist playlist);
        void Update(Playlist playlist);
        void Delete(int id);
        void AddSong(int playlistId, int cancionId);
        void RemoveSong(int playlistId, int cancionId);
    }
}