using CostaRicaMusic.DAL.Entities;
using CostaRicaMusic.DAL.Repositories;
using System;
using System.Collections.Generic;

namespace CostaRicaMusic.BLL.Services
{
    public class CancionService : ICancionService
    {
        private readonly ICancionRepository _cancionRepository;
        private readonly IArtistaRepository _artistaRepository;
        private readonly IAlbumRepository _albumRepository;

        public CancionService(
            ICancionRepository cancionRepository,
            IArtistaRepository artistaRepository,
            IAlbumRepository albumRepository)
        {
            _cancionRepository = cancionRepository;
            _artistaRepository = artistaRepository;
            _albumRepository = albumRepository;
        }

        public IEnumerable<Cancion> GetAll()
        {
            return _cancionRepository.GetAll();
        }

        public IEnumerable<Cancion> GetAllWithRelations()
        {
            return _cancionRepository.GetAllWithRelations();
        }

        public IEnumerable<Cancion> SearchByName(string busqueda)
        {
            return _cancionRepository.SearchByName(busqueda);
        }

        public IEnumerable<Cancion> GetPaged(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentException("El número de página debe ser mayor que cero.");

            if (pageSize <= 0)
                throw new ArgumentException("El tamaño de página debe ser mayor que cero.");

            return _cancionRepository.GetPaged(pageNumber, pageSize);
        }

        public Cancion? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id de la canción debe ser mayor que cero.");

            return _cancionRepository.GetById(id);
        }

        public void Add(Cancion cancion)
        {
            if (cancion == null)
                throw new ArgumentNullException(nameof(cancion));

            if (string.IsNullOrWhiteSpace(cancion.Nombre))
                throw new ArgumentException("El nombre de la canción es obligatorio.");

            if (cancion.ArtistaId <= 0)
                throw new ArgumentException("La canción debe tener un artista válido.");

            if (cancion.AlbumId <= 0)
                throw new ArgumentException("La canción debe tener un álbum válido.");

            var artistaExistente = _artistaRepository.GetById(cancion.ArtistaId);
            if (artistaExistente == null)
                throw new Exception("El artista asociado no existe.");

            var albumExistente = _albumRepository.GetById(cancion.AlbumId);
            if (albumExistente == null)
                throw new Exception("El álbum asociado no existe.");

            _cancionRepository.Add(cancion);
            _cancionRepository.Save();
        }

        public void Update(Cancion cancion)
        {
            if (cancion == null)
                throw new ArgumentNullException(nameof(cancion));

            if (cancion.Id <= 0)
                throw new ArgumentException("El id de la canción debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(cancion.Nombre))
                throw new ArgumentException("El nombre de la canción es obligatorio.");

            if (cancion.ArtistaId <= 0)
                throw new ArgumentException("La canción debe tener un artista válido.");

            if (cancion.AlbumId <= 0)
                throw new ArgumentException("La canción debe tener un álbum válido.");

            var cancionExistente = _cancionRepository.GetById(cancion.Id);
            if (cancionExistente == null)
                throw new Exception("La canción no existe.");

            var artistaExistente = _artistaRepository.GetById(cancion.ArtistaId);
            if (artistaExistente == null)
                throw new Exception("El artista asociado no existe.");

            var albumExistente = _albumRepository.GetById(cancion.AlbumId);
            if (albumExistente == null)
                throw new Exception("El álbum asociado no existe.");

            _cancionRepository.Update(cancion);
            _cancionRepository.Save();
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id de la canción debe ser mayor que cero.");

            var cancionExistente = _cancionRepository.GetById(id);
            if (cancionExistente == null)
                throw new Exception("La canción no existe.");

            _cancionRepository.Delete(id);
            _cancionRepository.Save();
        }
    }
}