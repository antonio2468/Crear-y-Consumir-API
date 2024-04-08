using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Proyecto_API.Data;
using Proyecto_API.DTO;
using Proyecto_API.Models;

namespace Proyecto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public  ActionResult<IEnumerable<UsuarioDTO>> GetUsuarios()
        {
            return Ok(UsuarioStore.usuarioList);
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
            var usuario = UsuarioStore.usuarioList.FirstOrDefault(v => v.Id == id);
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
            if (usuarioDTO == null)
            {
                return BadRequest(usuarioDTO);
            }
            if(usuarioDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            usuarioDTO.Id = UsuarioStore.usuarioList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            UsuarioStore.usuarioList.Add(usuarioDTO);

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
            var usuario = UsuarioStore.usuarioList.FirstOrDefault(v => v.Id == id);
            if(usuario == null)
            {
                return NotFound();
            }
            UsuarioStore.usuarioList.Remove(usuario);
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
            var usuario = UsuarioStore.usuarioList.FirstOrDefault(v => v.Id == id);
            usuario.Nombre = usuarioDTO.Nombre;
            usuario.Apellido= usuarioDTO.Apellido;
            usuario.Correo = usuarioDTO.Correo;
            usuario.Numero  = usuarioDTO.Numero;
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
            var usuario = UsuarioStore.usuarioList.FirstOrDefault(v => v.Id == id);

            patchDTO.ApplyTo(usuario, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
           
        }


    }
}
