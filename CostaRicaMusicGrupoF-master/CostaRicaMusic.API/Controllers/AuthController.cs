using CostaRicaMusic.API.Models;
using CostaRicaMusic.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CostaRicaMusic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Correo) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Debe enviar correo y contraseña."
                });
            }

            var usuario = _usuarioService.Login(request.Correo, request.Password);

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Correo o contraseña incorrectos."
                });
            }

            return Ok(new
            {
                success = true,
                userId = usuario.Id,
                nombre = usuario.Nombre
            });
        }
    }
}