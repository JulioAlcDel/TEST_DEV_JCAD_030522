
using System.Collections.Generic;
using System.Threading.Tasks;
using TokaEntidad;

namespace TokaRepositorio.Interface
{
    public interface IRepositorioPersonaFisica
    {
        Task<List<PersonaFisica>> GetPersonaFisicas();
        Task<PersonaFisica> GetPersonaFisicasBy(string id);
        Task<RespuestaSpPersonaFisica> AddPersonaFisica(PersonaFisica personaFisica);

        Task<RespuestaSpPersonaFisica> UpdatePersonaFisica(PersonaFisica personaFisica);
        Task<RespuestaSpPersonaFisica> DeletePersonaFisica(int id);

        Task<PersonaFisica> GetPersonaFisicasRFC(string rfc);
    }
}
