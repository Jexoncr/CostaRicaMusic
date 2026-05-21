using CostaRicaMusic.DAL.Entities;
using System.Collections.Generic;

namespace CostaRicaMusic.Web.Models
{
    public class ArtistaIndexViewModel
    {
        public IEnumerable<Artista> Artistas { get; set; } = new List<Artista>();
        public string? Busqueda { get; set; }
    }
}