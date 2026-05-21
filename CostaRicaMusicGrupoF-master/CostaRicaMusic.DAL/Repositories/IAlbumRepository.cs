using CostaRicaMusic.DAL.Entities;
using System.Collections.Generic;

namespace CostaRicaMusic.DAL.Repositories
{
    public interface IAlbumRepository : IRepository<Album>
    {
        IEnumerable<Album> GetAllWithArtista();
        IEnumerable<Album> GetByArtistaId(int artistaId);
        IEnumerable<Album> SearchByName(string? busqueda);
        Album? GetByIdWithSongs(int id);
    }
}