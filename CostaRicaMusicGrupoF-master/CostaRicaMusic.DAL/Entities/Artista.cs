using CostaRicaMusic.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostaRicaMusic.DAL.Entities
{
    public class Artista
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? ImagenUrl { get; set; }

        public ICollection<Album> Albumes { get; set; } = new List<Album>();
        public ICollection<Cancion> Canciones { get; set; } = new List<Cancion>();
    }
}
