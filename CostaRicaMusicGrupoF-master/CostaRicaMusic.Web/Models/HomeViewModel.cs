using CostaRicaMusic.DAL.Entities;

namespace CostaRicaMusic.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Playlist> Playlists { get; set; } = new List<Playlist>();
        public IEnumerable<Cancion> Canciones { get; set; } = new List<Cancion>();
        public IEnumerable<Artista> Artistas { get; set; } = new List<Artista>();
        public bool UsuarioLogueado { get; set; }
    }
}