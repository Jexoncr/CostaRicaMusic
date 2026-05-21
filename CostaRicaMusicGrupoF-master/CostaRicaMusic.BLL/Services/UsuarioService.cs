using System;
using System.Collections.Generic;
using System.Text;
using CostaRicaMusic.DAL.Entities;
using CostaRicaMusic.DAL.Repositories;

namespace CostaRicaMusic.BLL.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario? Login(string correo, string password)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException("El correo es obligatorio.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña es obligatoria.");

            var usuario = _usuarioRepository.GetByCorreo(correo);

            if (usuario == null)
                return null;

            if (usuario.PasswordHash != password)
                return null;

            return usuario;
        }
    }
}