using System;
using System.Collections.Generic;
using CostaRicaMusic.DAL.Entities;
using CostaRicaMusic.DAL.Repositories;

namespace CostaRicaMusic.BLL.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IPlaylistCancionRepository _playlistCancionRepository;
        private readonly ICancionRepository _cancionRepository;

        public PlaylistService(
            IPlaylistRepository playlistRepository,
            IPlaylistCancionRepository playlistCancionRepository,
            ICancionRepository cancionRepository)
        {
            _playlistRepository = playlistRepository;
            _playlistCancionRepository = playlistCancionRepository;
            _cancionRepository = cancionRepository;
        }

        public IEnumerable<Playlist> GetByUsuarioId(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("El id del usuario debe ser mayor que cero.");

            return _playlistRepository.GetByUsuarioId(usuarioId);
        }

        public Playlist? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id de la playlist debe ser mayor que cero.");

            return _playlistRepository.GetById(id);
        }

        public Playlist? GetByIdWithSongs(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id de la playlist debe ser mayor que cero.");

            return _playlistRepository.GetByIdWithSongs(id);
        }

        public void Add(Playlist playlist)
        {
            if (playlist == null)
                throw new ArgumentNullException(nameof(playlist));

            if (string.IsNullOrWhiteSpace(playlist.Nombre))
                throw new ArgumentException("El nombre de la playlist es obligatorio.");

            if (playlist.UsuarioId <= 0)
                throw new ArgumentException("La playlist debe tener un usuario válido.");

            playlist.FechaCreacion = DateTime.Now;

            _playlistRepository.Add(playlist);
            _playlistRepository.Save();
        }

        public void Update(Playlist playlist)
        {
            if (playlist == null)
                throw new ArgumentNullException(nameof(playlist));

            if (playlist.Id <= 0)
                throw new ArgumentException("El id de la playlist debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(playlist.Nombre))
                throw new ArgumentException("El nombre de la playlist es obligatorio.");

            var existente = _playlistRepository.GetById(playlist.Id);
            if (existente == null)
                throw new Exception("La playlist no existe.");

            existente.Nombre = playlist.Nombre;
            _playlistRepository.Update(existente);
            _playlistRepository.Save();
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id de la playlist debe ser mayor que cero.");

            var existente = _playlistRepository.GetById(id);
            if (existente == null)
                throw new Exception("La playlist no existe.");

            _playlistRepository.Delete(id);
            _playlistRepository.Save();
        }

        public void AddSong(int playlistId, int cancionId)
        {
            if (playlistId <= 0)
                throw new ArgumentException("El id de la playlist debe ser mayor que cero.");

            if (cancionId <= 0)
                throw new ArgumentException("El id de la canción debe ser mayor que cero.");

            var playlist = _playlistRepository.GetById(playlistId);
            if (playlist == null)
                throw new Exception("La playlist no existe.");

            var cancion = _cancionRepository.GetById(cancionId);
            if (cancion == null)
                throw new Exception("La canción no existe.");

            if (_playlistCancionRepository.Exists(playlistId, cancionId))
                throw new Exception("La canción ya existe en la playlist.");

            var relacion = new PlaylistCancion
            {
                PlaylistId = playlistId,
                CancionId = cancionId,
                Orden = _playlistCancionRepository.GetNextOrder(playlistId),
                FechaAgregado = DateTime.Now
            };

            _playlistCancionRepository.Add(relacion);
            _playlistCancionRepository.Save();
        }

        public void RemoveSong(int playlistId, int cancionId)
        {
            if (playlistId <= 0)
                throw new ArgumentException("El id de la playlist debe ser mayor que cero.");

            if (cancionId <= 0)
                throw new ArgumentException("El id de la canción debe ser mayor que cero.");

            var relacion = _playlistCancionRepository.GetByIds(playlistId, cancionId);
            if (relacion == null)
                throw new Exception("La canción no existe dentro de la playlist.");

            _playlistCancionRepository.Delete(relacion);
            _playlistCancionRepository.Save();
        }
    }
}