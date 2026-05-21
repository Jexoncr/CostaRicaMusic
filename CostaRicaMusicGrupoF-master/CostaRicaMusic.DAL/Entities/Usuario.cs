using CostaRicaMusic.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostaRicaMusic.DAL.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}