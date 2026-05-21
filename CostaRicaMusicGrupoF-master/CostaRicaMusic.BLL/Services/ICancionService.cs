using CostaRicaMusic.DAL.Entities;
using System.Collections.Generic;

namespace CostaRicaMusic.BLL.Services
{
    public interface ICancionService
    {
        IEnumerable<Cancion> GetAll();
        IEnumerable<Cancion> GetAllWithRelations();
        IEnumerable<Cancion> SearchByName(string nombre);
        IEnumerable<Cancion> GetPaged(int pageNumber, int pageSize);
        Cancion? GetById(int id);
        void Add(Cancion cancion);
        void Update(Cancion cancion);
        void Delete(int id);
    }
}