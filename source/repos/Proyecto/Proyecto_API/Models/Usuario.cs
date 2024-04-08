namespace Proyecto_API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Numero { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
