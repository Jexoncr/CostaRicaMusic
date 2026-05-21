using Microsoft.AspNetCore.Mvc;
using CostaRicaMusic.BLL.Services;
using CostaRicaMusic.Web.Models;

namespace CostaRicaMusic.Web.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        public IActionResult Index(string? busqueda)
        {
            var todos = _albumService.GetAllWithArtista();

            var albumes = string.IsNullOrWhiteSpace(busqueda)
                ? todos
                : _albumService.SearchByName(busqueda);

            var destacados = todos
                .OrderByDescending(a => a.Anio)
                .Take(4);

            var model = new AlbumIndexViewModel
            {
                Albumes = albumes,
                AlbumesDestacados = string.IsNullOrWhiteSpace(busqueda) ? destacados : Enumerable.Empty<CostaRicaMusic.DAL.Entities.Album>(),
                Busqueda = busqueda
            };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var album = _albumService.GetByIdWithSongs(id);

            if (album == null)
                return NotFound();

            return View(album);
        }
    }
}