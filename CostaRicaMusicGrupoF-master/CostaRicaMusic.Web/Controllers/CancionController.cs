using CostaRicaMusic.BLL.Services;
using CostaRicaMusic.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CostaRicaMusic.Web.Controllers
{
    public class CancionController : Controller
    {
        private readonly ICancionService _cancionService;
        private readonly IPlaylistService _playlistService;

        public CancionController(ICancionService cancionService, IPlaylistService playlistService)
        {
            _cancionService = cancionService;
            _playlistService = playlistService;
        }

        public IActionResult Index(string? busqueda, int pagina = 1)
        {
            const int pageSize = 5;

            var canciones = string.IsNullOrWhiteSpace(busqueda)
                ? _cancionService.GetAllWithRelations()
                : _cancionService.SearchByName(busqueda);

            var totalRegistros = canciones.Count();
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / pageSize);

            var cancionesPaginadas = canciones
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var userIdString = HttpContext.Session.GetString("UserId");
            bool usuarioLogueado = !string.IsNullOrEmpty(userIdString);

            var playlists = usuarioLogueado
                ? _playlistService.GetByUsuarioId(int.Parse(userIdString!))
                : Enumerable.Empty<CostaRicaMusic.DAL.Entities.Playlist>();

            var viewModel = new CancionIndexViewModel
            {
                Canciones = cancionesPaginadas,
                TodasLasCanciones = canciones,
                Busqueda = busqueda,
                PaginaActual = pagina,
                TotalPaginas = totalPaginas,
                TamanioPagina = pageSize,
                Playlists = playlists,
                UsuarioLogueado = usuarioLogueado
            };

            return View(viewModel);
        }
    }
}