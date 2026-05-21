using CostaRicaMusic.DAL.Entities;

namespace CostaRicaMusic.Web.Models
{
    public class CancionIndexViewModel
    {
        public List<Cancion> Canciones { get; set; } = new List<Cancion>();
        public IEnumerable<Cancion> TodasLasCanciones { get; set; } = new List<Cancion>();
        public IEnumerable<CostaRicaMusic.DAL.Entities.Playlist> Playlists { get; set; } = new List<CostaRicaMusic.DAL.Entities.Playlist>();

        public string? Busqueda { get; set; }

        public int PaginaActual { get; set; }

        public int TotalPaginas { get; set; }

        public int TamanioPagina { get; set; }

        public bool UsuarioLogueado { get; set; }
    }
}