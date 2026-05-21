using CostaRicaMusic.BLL.Services;
using CostaRicaMusic.DAL.Entities;
using CostaRicaMusic.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CostaRicaMusic.Web.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService _playlistService;
        private readonly ICancionService _cancionService;

        public PlaylistController(IPlaylistService playlistService, ICancionService cancionService)
        {
            _playlistService = playlistService;
            _cancionService = cancionService;
        }

        public IActionResult Index()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Auth");

            int usuarioId = int.Parse(userIdString);

            var playlists = _playlistService.GetByUsuarioId(usuarioId);
            return View(playlists);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Playlist playlist)
        {
            try
            {
                var userIdString = HttpContext.Session.GetString("UserId");

                if (string.IsNullOrEmpty(userIdString))
                    return RedirectToAction("Login", "Auth");

                playlist.UsuarioId = int.Parse(userIdString);

                _playlistService.Add(playlist);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(playlist);
            }
        }

        public IActionResult Edit(int id)
        {
            var playlist = _playlistService.GetById(id);

            if (playlist == null)
                return NotFound();

            return View(playlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Playlist playlist)
        {
            try
            {
                var userIdString = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userIdString))
                    return RedirectToAction("Login", "Auth");

                playlist.UsuarioId = int.Parse(userIdString);
                _playlistService.Update(playlist);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(playlist);
            }
        }

        public IActionResult Details(int id)
        {
            var playlist = _playlistService.GetByIdWithSongs(id);

            if (playlist == null)
                return NotFound();

            var songs = _cancionService.GetAll();

            var cancionesYaAgregadas = playlist.PlaylistCanciones
                .Select(pc => pc.CancionId)
                .ToList();

            var cancionesDisponibles = songs
                .Where(c => !cancionesYaAgregadas.Contains(c.Id))
                .ToList();

            var model = new PlaylistDetailsViewModel
            {
                Playlist = playlist,
                AvailableSongs = cancionesDisponibles
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSong(int playlistId, int cancionId, string? returnController = null, string? returnAction = null)
        {
            _playlistService.AddSong(playlistId, cancionId);
            TempData["SuccessMessage"] = "La canción se agregó correctamente a la playlist.";

            if (!string.IsNullOrEmpty(returnController) && !string.IsNullOrEmpty(returnAction))
            {
                return RedirectToAction(returnAction, returnController);
            }

            return RedirectToAction(nameof(Details), new { id = playlistId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveSong(int playlistId, int cancionId)
        {
            _playlistService.RemoveSong(playlistId, cancionId);
            return RedirectToAction(nameof(Details), new { id = playlistId });
        }

        public IActionResult Delete(int id)
        {
            var playlist = _playlistService.GetById(id);

            if (playlist == null)
                return NotFound();

            return View(playlist);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _playlistService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}