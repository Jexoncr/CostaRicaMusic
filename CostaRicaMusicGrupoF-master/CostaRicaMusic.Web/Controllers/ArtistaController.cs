using CostaRicaMusic.BLL.Services;
using CostaRicaMusic.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CostaRicaMusic.Web.Controllers
{
    public class ArtistaController : Controller
    {
        private readonly IArtistaService _artistaService;

        public ArtistaController(IArtistaService artistaService)
        {
            _artistaService = artistaService;
        }

        public IActionResult Index(string? busqueda)
        {
            var artistas = _artistaService.SearchByName(busqueda);

            var model = new ArtistaIndexViewModel
            {
                Artistas = artistas,
                Busqueda = busqueda
            };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var artista = _artistaService.GetByIdWithSongs(id);

            if (artista == null)
                return NotFound();

            return View(artista);
        }
    }
}