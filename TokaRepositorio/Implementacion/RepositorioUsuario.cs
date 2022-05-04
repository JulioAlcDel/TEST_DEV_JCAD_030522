using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokaDatos;
using TokaEntidad;
using TokaInfrestructura.Mapping;
using TokaRepositorio.Interface;

namespace TokaRepositorio.Implementacion
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        public readonly AplicationContext _context;
        public readonly MappingGeneric _mapping;
        public RepositorioUsuario(AplicationContext context, MappingGeneric mapping)
        {
            _context = context;
            _mapping = mapping;
        }
        public void AddUser(Usuario user)
        {
            _context.Tb_Usuario.Add(user);
            _context.SaveChanges();
                      
        }

        public async void DeleteUser(string id)
        {
                var entidad = GetUserById(id);
                _context.Entry(entidad).State = EntityState.Deleted;
               await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> GetUser()
        {
            return await _context.Tb_Usuario.ToListAsync();
        }

        public async Task<Usuario> GetUserById(string id)
        {
            return await _context.Tb_Usuario.Where(x => x.IdUsuario == int.Parse(id)).FirstOrDefaultAsync();
        }
        public async Task<Usuario> GetUserByEmail(string correo)
        {
            return await _context.Tb_Usuario.Where(x => x.Correo == correo).FirstOrDefaultAsync();
        }
        public async Task<Usuario> GetUserByToken(string token)
        {
            return await _context.Tb_Usuario.Where(x => x.Token == token).FirstOrDefaultAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();

                }
            }
        }


    }
}
