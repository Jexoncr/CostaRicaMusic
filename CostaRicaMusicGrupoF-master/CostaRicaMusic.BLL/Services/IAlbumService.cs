using CostaRicaMusic.DAL.Entities;
using System.Collections.Generic;

namespace CostaRicaMusic.BLL.Services
{
    public interface IAlbumService
    {
        IEnumerable<Album> GetAll();
        IEnumerable<Album> GetAllWithArtista();
        IEnumerable<Album> GetByArtistaId(int artistaId);
        IEnumerable<Album> SearchByName(string? busqueda);
        Album? GetById(int id);
        Album? GetByIdWithSongs(int id);
        void Add(Album album);
        void Update(Album album);
        void Delete(int id);
    }
}