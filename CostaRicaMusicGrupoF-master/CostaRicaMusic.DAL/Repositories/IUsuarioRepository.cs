using System;
using System.Collections.Generic;
using System.Text;
using CostaRicaMusic.DAL.Entities;

namespace CostaRicaMusic.DAL.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario? GetByCorreo(string correo);
    }
}