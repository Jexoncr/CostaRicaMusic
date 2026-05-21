using CostaRicaMusic.DAL.Entities;
using CostaRicaMusic.DAL.Repositories;
using System;
using System.Collections.Generic;

namespace CostaRicaMusic.BLL.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistaRepository _artistaRepository;

        public AlbumService(IAlbumRepository albumRepository, IArtistaRepository artistaRepository)
        {
            _albumRepository = albumRepository;
            _artistaRepository = artistaRepository;
        }

        public IEnumerable<Album> GetAll()
        {
            return _albumRepository.GetAll();
        }

        public IEnumerable<Album> GetAllWithArtista()
        {
            return _albumRepository.GetAllWithArtista();
        }

        public IEnumerable<Album> GetByArtistaId(int artistaId)
        {
            if (artistaId <= 0)
                throw new ArgumentException("El id del artista debe ser mayor que cero.");

            return _albumRepository.GetByArtistaId(artistaId);
        }

        public IEnumerable<Album> SearchByName(string? busqueda)
        {
            return _albumRepository.SearchByName(busqueda);
        }

        public Album? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id del álbum debe ser mayor que cero.");

            return _albumRepository.GetById(id);
        }

        public Album? GetByIdWithSongs(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id del álbum debe ser mayor que cero.");

            return _albumRepository.GetByIdWithSongs(id);
        }

        public void Add(Album album)
        {
            if (album == null)
                throw new ArgumentNullException(nameof(album));

            if (string.IsNullOrWhiteSpace(album.Titulo))
                throw new ArgumentException("El título del álbum es obligatorio.");

            if (album.ArtistaId <= 0)
                throw new ArgumentException("El álbum debe estar ligado a un artista válido.");

            var artistaExistente = _artistaRepository.GetById(album.ArtistaId);
            if (artistaExistente == null)
                throw new Exception("El artista asociado no existe.");

            _albumRepository.Add(album);
            _albumRepository.Save();
        }

        public void Update(Album album)
        {
            if (album == null)
                throw new ArgumentNullException(nameof(album));

            if (album.Id <= 0)
                throw new ArgumentException("El id del álbum debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(album.Titulo))
                throw new ArgumentException("El título del álbum es obligatorio.");

            if (album.ArtistaId <= 0)
                throw new ArgumentException("El álbum debe estar ligado a un artista válido.");

            var albumExistente = _albumRepository.GetById(album.Id);
            if (albumExistente == null)
                throw new Exception("El álbum no existe.");

            var artistaExistente = _artistaRepository.GetById(album.ArtistaId);
            if (artistaExistente == null)
                throw new Exception("El artista asociado no existe.");

            _albumRepository.Update(album);
            _albumRepository.Save();
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id del álbum debe ser mayor que cero.");

            var albumExistente = _albumRepository.GetById(id);
            if (albumExistente == null)
                throw new Exception("El álbum no existe.");

            _albumRepository.Delete(id);
            _albumRepository.Save();
        }
    }
}