using CostaRicaMusic.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CostaRicaMusic.DAL.Entities
{
    public class Playlist
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }

        public Usuario? Usuario { get; set; }
        public ICollection<PlaylistCancion> PlaylistCanciones { get; set; } = new List<PlaylistCancion>();
    }
}