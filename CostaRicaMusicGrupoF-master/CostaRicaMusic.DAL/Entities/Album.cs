using CostaRicaMusic.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostaRicaMusic.DAL.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int ArtistaId { get; set; }
        public int Anio { get; set; }
        public string? PortadaUrl { get; set; }

        public Artista? Artista { get; set; }
        public ICollection<Cancion> Canciones { get; set; } = new List<Cancion>();
    }
}