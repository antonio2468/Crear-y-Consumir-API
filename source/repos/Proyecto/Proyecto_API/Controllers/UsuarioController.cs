using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API.Data;
using Proyecto_API.DTO;
using Proyecto_API.Models;

namespace Proyecto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly ApplicationDbContext _dbcontext;
        public UsuarioController( ILogger<UsuarioController> logger, ApplicationDbContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public  ActionResult<IEnumerable<UsuarioDTO>> GetUsuarios()
        {
            _logger.LogInformation("Obtener los Usuarios");
            return Ok(_dbcontext.Usuarios.ToList());
        }

        [HttpGet ("id:int", Name="GetUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsuarioDTO> GetUsuario(int id) 
        {
            if(id == 0)
            {
                return BadRequest();
            }
            //var usuario = UsuarioStore.usuarioList.FirstOrDefault(v => v.Id == id);
            var usuario=_dbcontext.Usuarios.FirstOrDefault(x => x.Id == id);
            if(usuario ==null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UsuarioDTO> CrearUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_dbcontext.Usuarios.FirstOrDefault(x => x.Nombre.ToLower() == usuarioDTO.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "El Usuario con ese Nombre ya Existe!");
                return BadRequest(ModelState);
            }
            if(usuarioDTO == null)
            {
                return BadRequest(usuarioDTO);
            }
            if(usuarioDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Usuario model = new()
            {
                Nombre = usuarioDTO.Nombre,
                Apellido = usuarioDTO.Apellido,
                Correo = usuarioDTO.Correo,
                Numero = usuarioDTO.Numero,
                ImagenUrl = usuarioDTO.ImageUrl,

            };
           _dbcontext.Usuarios.Add(model);
            _dbcontext.SaveChanges();
            return CreatedAtRoute("GetUsuario", new {id= usuarioDTO.Id}, usuarioDTO);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUsuario(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var usuario = _dbcontext.Usuarios.FirstOrDefault(v => v.Id == id);
            if(usuario == null)
            {
                return NotFound();
            }
            _dbcontext.Usuarios.Remove(usuario);
            _dbcontext.SaveChanges();
            return NoContent();
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            if(usuarioDTO == null || id!=usuarioDTO.Id)
            {
                return BadRequest();
            }
            Usuario modelo = new()
            {
                Id = usuarioDTO.Id,
                Nombre = usuarioDTO.Nombre,
                Apellido = usuarioDTO.Apellido,
                Numero = usuarioDTO.Numero,
                Correo = usuarioDTO.Correo,
                ImagenUrl = usuarioDTO.ImageUrl,
            };
            _dbcontext.Usuarios.Update(modelo);
            _dbcontext.SaveChanges();
            return NoContent();
        }




        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateParcialUsuario(int id, JsonPatchDocument<UsuarioDTO> patchDTO)
        {
            if (patchDTO == null || id == id)
            {
                return BadRequest();
            }
            //var usuario = UsuarioStore.usuarioList.FirstOrDefault(v => v.Id == id);
            var usuario =_dbcontext.Usuarios.AsNoTracking().FirstOrDefault(u => u.Id == id);
            UsuarioDTO usuariodto = new()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido=usuario.Apellido,
                Numero = usuario.Numero,
                Correo=usuario.Correo,
                ImageUrl=usuario.ImagenUrl

            };
            if (usuario == null) return BadRequest();

            patchDTO.ApplyTo(usuariodto, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Usuario modelo = new()
            {
                Id = usuariodto.Id,
                Nombre = usuariodto.Nombre,
                Apellido = usuariodto.Apellido,
                Numero = usuariodto.Numero,
                Correo = usuariodto.Correo,
                ImagenUrl = usuariodto.ImageUrl
            };
            _dbcontext.Usuarios.Update(modelo);
            _dbcontext.SaveChanges();
            return NoContent();
           
        }


    }
}
