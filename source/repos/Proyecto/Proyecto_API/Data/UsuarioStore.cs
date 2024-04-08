using Proyecto_API.DTO;

namespace Proyecto_API.Data
{
    public static class UsuarioStore
    {
        public static List<UsuarioDTO> usuarioList = new List<UsuarioDTO>
        {
            new UsuarioDTO{Id=1, Nombre="Jose", Apellido="Cortes", Correo="cortesantonio822@gmail.com", Numero="4494597406"},
            new UsuarioDTO{Id=2, Nombre="Antonio", Apellido="Puente", Correo="cortesantonio820@gmail.com", Numero="4494597407"}

        };
    }
}
