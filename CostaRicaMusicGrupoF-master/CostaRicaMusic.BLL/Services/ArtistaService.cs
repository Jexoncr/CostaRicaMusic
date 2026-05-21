using CostaRicaMusic.DAL.Entities;
using CostaRicaMusic.DAL.Repositories;
using System;
using System.Collections.Generic;

namespace CostaRicaMusic.BLL.Services
{
    public class ArtistaService : IArtistaService
    {
        private readonly IArtistaRepository _artistaRepository;

        public ArtistaService(IArtistaRepository artistaRepository)
        {
            _artistaRepository = artistaRepository;
        }

        public IEnumerable<Artista> GetAll()
        {
            return _artistaRepository.GetAll();
        }

        public IEnumerable<Artista> GetAllWithAlbums()
        {
            return _artistaRepository.GetAllWithAlbums();
        }

        public Artista? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id del artista debe ser mayor que cero.");

            return _artistaRepository.GetById(id);
        }

        public void Add(Artista artista)
        {
            if (artista == null)
                throw new ArgumentNullException(nameof(artista));

            if (string.IsNullOrWhiteSpace(artista.Nombre))
                throw new ArgumentException("El nombre del artista es obligatorio.");

            _artistaRepository.Add(artista);
            _artistaRepository.Save();
        }

        public void Update(Artista artista)
        {
            if (artista == null)
                throw new ArgumentNullException(nameof(artista));

            if (artista.Id <= 0)
                throw new ArgumentException("El id del artista debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(artista.Nombre))
                throw new ArgumentException("El nombre del artista es obligatorio.");

            var artistaExistente = _artistaRepository.GetById(artista.Id);
            if (artistaExistente == null)
                throw new Exception("El artista no existe.");

            _artistaRepository.Update(artista);
            _artistaRepository.Save();
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id del artista debe ser mayor que cero.");

            var artistaExistente = _artistaRepository.GetById(id);
            if (artistaExistente == null)
                throw new Exception("El artista no existe.");

            _artistaRepository.Delete(id);
            _artistaRepository.Save();
        }

        public Artista? GetByIdWithSongs(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id del artista debe ser mayor que cero.");

            return _artistaRepository.GetByIdWithSongs(id);
        }
        public IEnumerable<Artista> SearchByName(string? busqueda)
        {
            return _artistaRepository.SearchByName(busqueda);
        }
    }
}