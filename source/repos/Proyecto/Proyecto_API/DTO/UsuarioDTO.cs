using System.ComponentModel.DataAnnotations;

namespace Proyecto_API.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Correo { get; set; }
        public string Numero { get; set; }

        public string ImageUrl { get; set; }
    }
}
