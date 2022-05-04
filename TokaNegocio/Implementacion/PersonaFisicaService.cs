using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokaDTOS;
using TokaEntidad;
using TokaInfrestructura.Halper;
using TokaInfrestructura.Mapping;
using TokaNegocio.Interface;
using TokaRepositorio.Interface;

namespace TokaNegocio.Implementacion
{
    public class PersonaFisicaService : IPersonaFisicaService
    {
        public readonly IRepositorioPersonaFisica _repositorio;
        public readonly MappingGeneric _mapping;
        public PersonaFisicaService(IRepositorioPersonaFisica repositorio, MappingGeneric mapping)
        {
            _repositorio = repositorio;
            _mapping = mapping;
        }

        public async Task<ReturnHelper<PersonaFisica>> AddPersona(PersonaFisicaDTO persona)
        {
            var resultado = new ReturnHelper<PersonaFisica>();
            try
            {
                var respuestaSp = _mapping.MappingErrorDTO(_repositorio.AddPersonaFisica(_mapping.MappingPersonaFisica(persona)).Result);
                if (respuestaSp.Success == true)
                {
                    var mapPersona = await _repositorio.GetPersonaFisicasRFC(persona.RFC); 
                    resultado.Value = mapPersona;
                    resultado.MenssageSucess = respuestaSp.MensajeError;
                }
                else
                {
                    resultado.AddError(respuestaSp.MensajeError);
                }
                return resultado;
            }
            catch (Exception e)
            {

                resultado.AddError("Ocurrio un error de sistema ");
                return resultado;
            }
        }
        public async Task<ReturnHelper<PersonaFisica>> UpdatePersona(PersonaFisicaDTO persona)
        {
            var resultado = new ReturnHelper<PersonaFisica>();
            try
            {
                var respuestaSp = _mapping.MappingErrorDTO(_repositorio.UpdatePersonaFisica(_mapping.MappingPersonaFisica(persona)).Result);
                if (respuestaSp.Success == true)
                {
                    var mapPersona = await _repositorio.GetPersonaFisicasBy(persona.IdPersonaFisica); ;
                    resultado.Value = mapPersona;
                    resultado.MenssageSucess = respuestaSp.MensajeError;
                }
                else
                {
                    resultado.AddError(respuestaSp.MensajeError);
                }
                return resultado;
            }
            catch (Exception e)
            {

                resultado.AddError("Ocurrio un error de sistema ");
                return resultado;
            }
        }
        public async Task<ReturnHelper<List<PersonaFisica>>> GetAllPersona()
        {
            var resultado = new ReturnHelper<List<PersonaFisica>> ();
            try
            {
                var list = await _repositorio.GetPersonaFisicas();
                resultado.Value = list;
                return resultado;
            }
            catch(Exception e)
            {

                resultado.AddError("Ocurrio un error al consultar ");
                return resultado;
            }

        }


        public async Task<ReturnHelper<PersonaFisica>> GetPersonaFisicaById(string id)
        {
            var resultado = new ReturnHelper<PersonaFisica>();
            try
            {
                var personaFisica = await _repositorio.GetPersonaFisicasBy(id);
                if (personaFisica is null)
                {
                    resultado.AddError("La persona fisica no existe");

                }
                else
                {
                    resultado.Value = personaFisica;
                }
                return resultado;

            } catch(Exception e){

                resultado.AddError("Ocurrio un error al consultar una Persona Fisica ");
                return resultado;
            }

        }

        public async Task<ReturnHelper<PersonaFisica>> DeletePersona(string id)
        {
            var resultado = new ReturnHelper<PersonaFisica>();
            try
            {
                var mapPersona = await _repositorio.GetPersonaFisicasBy(id); 
                var respuestaSp = _mapping.MappingErrorDTO(_repositorio.DeletePersonaFisica(int.Parse(id)).Result);
                if (respuestaSp.Success == true)
                { 
                   
                    resultado.Value = mapPersona;
                    resultado.MenssageSucess = respuestaSp.MensajeError;

                }
                else
                {
                    resultado.AddError(respuestaSp.MensajeError);
                }
                return resultado;
            }
            catch (Exception e)
            {
                resultado.AddError("Ocurrio un error al consultar una Persona Fisica ");
                return resultado;
            }
        }
    }
}
