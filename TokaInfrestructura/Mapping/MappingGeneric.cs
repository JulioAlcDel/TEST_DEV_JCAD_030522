
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaDTOS;
using TokaEntidad;

namespace TokaInfrestructura.Mapping
{
    public class MappingGeneric
    {

        public PersonaFisica MappingPersonaFisica(PersonaFisicaDTO dto)
        {
            return new PersonaFisica()
            {
                IdPersonaFisica = dto.IdPersonaFisica == null ? 0: int.Parse(dto.IdPersonaFisica),
                Nombre = dto.Nombre,
                ApellidoMaterno = dto.ApellidoMaterno,
                ApellidoPaterno = dto.ApellidoPaterno,
                RFC = dto.RFC,
                FechaNacimiento = dto.FechaNacimiento,
                FechaActualizacion = DateTime.Now,
                UsuarioAgrega = 1,
                Activo = true
            };
        
        }

        public ErrorDTO MappingErrorDTO(RespuestaSpPersonaFisica respuesta)
        {
            return  new ErrorDTO(){
                MensajeError = respuesta.MENSAJEERROR,
                Error = respuesta.ERROR
            };
        }
        public List<PersonaFisicaDTO> MappingListPersonasFisicas(List<PersonaFisica> list)
        {
            return list.Select(x => new PersonaFisicaDTO()
            {
                IdPersonaFisica = x.IdPersonaFisica.ToString() ,
                Nombre = x.Nombre,
                ApellidoMaterno = x.ApellidoMaterno,
                ApellidoPaterno = x.ApellidoPaterno,
                RFC = x.RFC,
                UsuarioAgrega = x.UsuarioAgrega,
                Activo = x.Activo
            }).ToList();
        }
        public Usuario MappingUser(UsuarioDTO usuario)
        {
            return new Usuario()
            {
                IdUsuario = usuario.IdUsuario,
                Clave     = usuario.Clave,
                Correo    = usuario.Correo,
                Sal       = usuario.Sal,
                Token     = usuario.Token
            };
        }

        public List<UsuarioDTO> MappinglistUser(List<Usuario> list)
        {
            return list.Select(x => new UsuarioDTO
            {
                IdUsuario = x.IdUsuario,
                Clave = x.Clave,
                Correo = x.Correo,
                Sal = x.Sal,
                Token = x.Token
            }).ToList();
        }
        public UsuarioDTO MappingUserDTO(Usuario usuario)
        {
            return new UsuarioDTO()
            {
                IdUsuario = usuario.IdUsuario,
                Clave = usuario.Clave,
                Correo = usuario.Correo,
                Sal = usuario.Sal,
                Token = usuario.Token
            };
        }

    }
}
