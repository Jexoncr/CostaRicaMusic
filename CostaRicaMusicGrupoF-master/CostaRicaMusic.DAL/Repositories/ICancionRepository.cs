using CostaRicaMusic.DAL.Entities;
using System.Collections.Generic;

namespace CostaRicaMusic.DAL.Repositories
{
    public interface ICancionRepository : IRepository<Cancion>
    {
        IEnumerable<Cancion> GetAllWithRelations();
        IEnumerable<Cancion> SearchByName(string nombre);
        IEnumerable<Cancion> GetPaged(int pageNumber, int pageSize);
    }
}