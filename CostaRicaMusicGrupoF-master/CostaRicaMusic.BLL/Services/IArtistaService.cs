using CostaRicaMusic.DAL.Entities;
using System.Collections.Generic;

namespace CostaRicaMusic.BLL.Services
{
    public interface IArtistaService
    {
        IEnumerable<Artista> GetAll();
        IEnumerable<Artista> GetAllWithAlbums();
        Artista? GetById(int id);
        void Add(Artista artista);
        void Update(Artista artista);
        void Delete(int id);
        Artista? GetByIdWithSongs(int id);
        IEnumerable<Artista> SearchByName(string? busqueda);

    }
}