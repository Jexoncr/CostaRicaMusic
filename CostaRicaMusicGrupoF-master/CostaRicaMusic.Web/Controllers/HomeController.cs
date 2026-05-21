using CostaRicaMusic.BLL.Services;
using CostaRicaMusic.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CostaRicaMusic.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlaylistService _playlistService;
        private readonly ICancionService _cancionService;
        private readonly IArtistaService _artistaService;

        public HomeController(
            IPlaylistService playlistService,
            ICancionService cancionService,
            IArtistaService artistaService)
        {
            _playlistService = playlistService;
            _cancionService = cancionService;
            _artistaService = artistaService;
        }

        public IActionResult Index()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            bool usuarioLogueado = !string.IsNullOrEmpty(userIdString);

            IEnumerable<CostaRicaMusic.DAL.Entities.Playlist> playlists = new List<CostaRicaMusic.DAL.Entities.Playlist>();

            if (usuarioLogueado)
            {
                int usuarioId = int.Parse(userIdString);
                playlists = _playlistService.GetByUsuarioId(usuarioId).Take(6);
            }

            var canciones = _cancionService.GetAllWithRelations().Take(10);
            var artistas = _artistaService.SearchByName(null).Take(6);

            var model = new HomeViewModel
            {
                UsuarioLogueado = usuarioLogueado,
                Playlists = playlists,
                Canciones = canciones,
                Artistas = artistas
            };

            return View(model);
        }
    }
}