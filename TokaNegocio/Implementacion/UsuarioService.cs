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
    public class UsuarioService : IUsuarioService
    {
        public readonly IRepositorioUsuario _repositorio;
        public readonly MappingGeneric _mapping;
        public UsuarioService(IRepositorioUsuario repositorio, MappingGeneric mapping)
        {
            _repositorio = repositorio;
            _mapping = mapping; 
        }

        public async Task<ReturnHelper<Usuario>> AddUser(UsuarioDTO usuario)
        {
            var resultado = new ReturnHelper<Usuario>();
            try
            {
                var entidad = _mapping.MappingUser(usuario);
                _repositorio.AddUser(entidad);
                var usuarioCons = _repositorio.GetUser().Result.Where(x => x.Correo == usuario.Correo).FirstOrDefault();
              
                if (usuarioCons is null)
                {
                    resultado.AddError("Ocurrio un error al guardar el registro ");
                }
                else
                {
                    resultado.Value = usuarioCons;
                    resultado.MenssageSucess = "Usuario agregado exitosamente";
                }

                return resultado;
            }
            catch (Exception e)
            {
                resultado.AddError("Ocurrio un error de sistema ");
                return resultado;
            }
        }
        public async Task<ReturnHelper<Usuario>> UpdateUser(UsuarioDTO usuario)
        {
            var resultado = new ReturnHelper<Usuario>();
            try
            {
                var usuarioAnt = await _repositorio.GetUserById(usuario.IdUsuario.ToString());
                var entidad = _mapping.MappingUser(usuario);
              //  _repositorio.Upda(entidad);
                var usuarioCons = await _repositorio.GetUserByEmail(usuario.Correo);
                if (usuarioAnt.Equals(usuarioCons)){
                    resultado.Value = usuarioCons;
                    resultado.MenssageSucess = "Usuario actualizado exitosamente";
                }
                return resultado;
            }
            catch (Exception e)
            {
                resultado.AddError("Ocurrio un error de sistema ");
                return resultado;
            }
        }
        public async Task<ReturnHelper<Usuario>> DeleteUser(string id)
        {
            var resultado = new ReturnHelper<Usuario>();
            try
            {
                var usuarioCons = await _repositorio.GetUserById(id);
                _repositorio.DeleteUser(id);
                var usuario = await _repositorio.GetUserById(id);
                if (usuarioCons is null)
                {
                    resultado.AddError("Este usuario no existe");
                }
                if (usuario is null)
                {
                    resultado.Value = usuarioCons;
                    resultado.MenssageSucess = "Se elimino correctamente el usuario";
                }


                return resultado;
            }
            catch (Exception e)
            {
                resultado.AddError("Ocurrio un error de sistema ");
                return resultado;
            }
        }

        public async Task<ReturnHelper<Usuario>> GetUserById(string id)
        {
            var resultado = new ReturnHelper<Usuario>();
            try
            {
                var usuario = await _repositorio.GetUserById(id);
                if(usuario is null)
                {
                    resultado.AddError("El usuario no existe");
                }
                else
                {
                    resultado.Value = usuario;
                }
                return resultado;
            }
            catch (Exception e)
            {
                resultado.AddError("Ocurrio un error de sistema ");
                return resultado;
            }
        }
        public async Task<ReturnHelper<Usuario>> GetUserByEmail(string correo)
        {
            var resultado = new ReturnHelper<Usuario>();
            try
            {
                var usuario = await _repositorio.GetUserByEmail(correo);
                if (usuario is null)
                {
                    resultado.AddError("El usuario no existe");
                }
                else
                {
                    resultado.Value = usuario;
                }
                return resultado;
            }
            catch (Exception e)
            {
                resultado.AddError("Ocurrio un error de sistema ");
                return resultado;
            }
        }
        public async Task<ReturnHelper<Usuario>> GetUserByToken(string token)
        {
            var resultado = new ReturnHelper<Usuario>();
            try
            {
                var usuario = await _repositorio.GetUserByToken(token);
                if (usuario is null)
                {
                    resultado.AddError("El usuario no existe");
                }
                else
                {
                    resultado.Value = usuario;
                }
                return resultado;
            }
            catch (Exception e)
            {
                resultado.AddError("Ocurrio un error de sistema ");
                return resultado;
            }
        }
        public async Task<ReturnHelper<List<Usuario>>> GetUsers()
        {
            var resultado = new ReturnHelper<List<Usuario>>();
            try 
            {
                var usuario = await _repositorio.GetUser();
                resultado.Value = usuario;
                return resultado;
            }

            catch (Exception e)
            {
                resultado.AddError("Ocurrio un error de sistema ");
                return resultado;
            }
        }


    }
}
