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
    public class RepositorioPersonaFisica : IRepositorioPersonaFisica,IDisposable
    {
        public readonly AplicationContext _context;
        public readonly MappingGeneric _mapping;
        public RepositorioPersonaFisica(AplicationContext context, MappingGeneric mapping)
        {
            _context = context;
            _mapping = mapping;
        }
        public async Task<RespuestaSpPersonaFisica> AddPersonaFisica(PersonaFisica personaFisica)
        {
            return await _context.AddPersonFisic(personaFisica);
        }

        public async Task<RespuestaSpPersonaFisica> DeletePersonaFisica(int id)
        {
            return await _context.DeletePersonFisic(id);
        }

        public async Task<List<PersonaFisica>> GetPersonaFisicas()
        {
            return await _context.Tb_PersonaFisicas.Where(x=> x.Activo == true).ToListAsync();
        }
        public async Task<PersonaFisica> GetPersonaFisicasBy(string id)
        {
            return await _context.Tb_PersonaFisicas.Where(x => x.IdPersonaFisica == int.Parse(id)).FirstOrDefaultAsync();
        }
        public async Task<PersonaFisica> GetPersonaFisicasRFC(string rfc)
        {
            return await _context.Tb_PersonaFisicas.Where(x => x.RFC == rfc).FirstOrDefaultAsync();
        }
        public async Task<RespuestaSpPersonaFisica> UpdatePersonaFisica(PersonaFisica personaFisica)
        {
            return await _context.UpdatePersonFisic(personaFisica);
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
