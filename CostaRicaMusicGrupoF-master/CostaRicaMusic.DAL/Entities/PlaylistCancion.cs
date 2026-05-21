using CostaRicaMusic.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CostaRicaMusic.DAL.Entities
{
    public class PlaylistCancion
    {
        public int PlaylistId { get; set; }
        public int CancionId { get; set; }
        public int Orden { get; set; }
        public DateTime FechaAgregado { get; set; } = DateTime.Now;

        public Playlist? Playlist { get; set; }
        public Cancion? Cancion { get; set; }
    }
}