using System;
using System.Collections.Generic;
using System.Text;
using CostaRicaMusic.DAL.Context;
using CostaRicaMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CostaRicaMusic.DAL.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(CostaRicaMusicContext context) : base(context)
        {
        }

        public Usuario? GetByCorreo(string correo)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Correo == correo);
        }
    }
}