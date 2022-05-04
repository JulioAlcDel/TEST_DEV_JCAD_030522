using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaDTOS;
using TokaNegocio.Interface;

namespace TokaPruebaApi.Controllers
{
    //[Authorize(Policy= "UserToken")]
    [Authorize]
    [ApiController]
    [Route("api/personaFisica")]
    public class PersonaFisicaController : Controller
    {
        private readonly IPersonaFisicaService _personaFisica;
        public PersonaFisicaController(IPersonaFisicaService personaFisica)
        {
            _personaFisica = personaFisica;
        }
        
        public async Task<IActionResult> Get()
        {
            var result = await _personaFisica.GetAllPersona();
            if (result.Success == false)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _personaFisica.GetPersonaFisicaById(id.ToString());
            if (result.Success == false)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(PersonaFisicaDTO persona)
        {
            var result = await _personaFisica.AddPersona(persona);
            if (result.Success == false)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personaFisica.DeletePersona(id.ToString());
            if (result.Success == false)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, PersonaFisicaDTO persona)
        {
            if(id.ToString() != persona.IdPersonaFisica)
            {
                return BadRequest("No concide las identificadores");
            }
            var personaFIsica = await _personaFisica.GetPersonaFisicaById(id.ToString());
            if(personaFIsica.Success == false)
            {
                return NotFound(persona);
            }
            
            var result = await _personaFisica.UpdatePersona(persona);
            if (result.Success == false)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
