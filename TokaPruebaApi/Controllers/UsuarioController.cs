using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaDTOS;
using TokaInfrestructura.Halper;
using TokaNegocio.Interface;
using TokaPruebaApi.Models;

namespace TokaPruebaApi.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuario;
        public UsuarioController(IUsuarioService usuario)
        {
            _usuario = usuario;
        }

        public async Task<IActionResult> Get()
        {
            var usuarios =await _usuario.GetUsers();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var usuario = await _usuario.GetUserById(id.ToString());
            if (usuario.Success != true)
            {
                return NotFound(usuario);
            }
            else
            {
                return Ok(usuario);
            }

        }

        [HttpPost]

        public async Task<IActionResult> Post(UsuarioDTO usuario)
        {
            var model = await _usuario.GetUserByEmail(usuario.Correo);
            if (model.Value is not null)
            {
                model.AddError("El usuario ya existe");
                return BadRequest(model);
            }

            HashPassword Password = HashHelper.Hash(usuario.Clave);
            usuario.Clave = Password.Password;
            usuario.Sal = Password.Salt;
            usuario.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var addUser =await _usuario.AddUser(usuario);
            if(addUser.Success == false)
            {
                return BadRequest(addUser);
            }
            return CreatedAtAction(nameof(Get), new { id = addUser.Value.IdUsuario }, new UserViewModel()
            {
                IdUsuario = addUser.Value.IdUsuario,
                Correo = addUser.Value.Correo,
                Token = addUser.Value.Token
            });
        }
    }
}
