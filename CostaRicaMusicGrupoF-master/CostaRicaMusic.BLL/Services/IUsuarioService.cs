using System;
using System.Collections.Generic;
using System.Text;
using CostaRicaMusic.DAL.Entities;

namespace CostaRicaMusic.BLL.Services
{
    public interface IUsuarioService
    {
        Usuario? Login(string correo, string password);
    }
}