using CostaRicaMusic.DAL.Entities;
using System.Collections.Generic;

namespace CostaRicaMusic.DAL.Repositories
{
    public interface IArtistaRepository : IRepository<Artista>
    {
        IEnumerable<Artista> GetAllWithAlbums();
        Artista? GetByIdWithSongs(int id);
        IEnumerable<Artista> SearchByName(string? busqueda);
    }
}