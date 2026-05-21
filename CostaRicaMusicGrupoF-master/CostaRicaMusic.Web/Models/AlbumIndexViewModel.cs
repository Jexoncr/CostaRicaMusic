using CostaRicaMusic.DAL.Entities;

namespace CostaRicaMusic.Web.Models
{
    public class AlbumIndexViewModel
    {
        public IEnumerable<Album> Albumes { get; set; } = new List<Album>();
        public IEnumerable<Album> AlbumesDestacados { get; set; } = new List<Album>();
        public string? Busqueda { get; set; }
    }
}
