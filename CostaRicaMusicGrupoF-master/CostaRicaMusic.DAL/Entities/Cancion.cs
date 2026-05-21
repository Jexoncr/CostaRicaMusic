using CostaRicaMusic.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostaRicaMusic.DAL.Entities
{
    public class Cancion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int AlbumId { get; set; }
        public int ArtistaId { get; set; }
        public int Duracion { get; set; } // en segundos
        public string AudioUrl { get; set; } = string.Empty;

        public Album? Album { get; set; }
        public Artista? Artista { get; set; }
        public ICollection<PlaylistCancion> PlaylistCanciones { get; set; } = new List<PlaylistCancion>();
    }
}