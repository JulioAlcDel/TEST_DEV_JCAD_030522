
using System.Collections.Generic;
using System.Threading.Tasks;
using TokaDTOS;
using TokaEntidad;
using TokaInfrestructura.Halper;

namespace TokaNegocio.Interface
{
    public interface IPersonaFisicaService
    {
        Task<ReturnHelper<PersonaFisica>> GetPersonaFisicaById(string id);
        Task<ReturnHelper<List<PersonaFisica>>> GetAllPersona();
        Task<ReturnHelper<PersonaFisica>> AddPersona(PersonaFisicaDTO persona);
        Task<ReturnHelper<PersonaFisica>> UpdatePersona(PersonaFisicaDTO persona);
        Task<ReturnHelper<PersonaFisica>> DeletePersona(string id);


    }
}
