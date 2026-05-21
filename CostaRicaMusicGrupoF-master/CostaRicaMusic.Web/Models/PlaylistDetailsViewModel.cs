using CostaRicaMusic.DAL.Entities;
using System.Collections.Generic;

namespace CostaRicaMusic.Web.Models
{
    public class PlaylistDetailsViewModel
    {
        public Playlist Playlist { get; set; } = new Playlist();
        public IEnumerable<Cancion> AvailableSongs { get; set; } = new List<Cancion>();
    }
}