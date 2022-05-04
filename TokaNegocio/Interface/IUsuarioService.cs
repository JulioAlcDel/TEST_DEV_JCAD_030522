using System.Collections.Generic;
using System.Threading.Tasks;
using TokaDTOS;
using TokaEntidad;
using TokaInfrestructura.Halper;

namespace TokaNegocio.Interface
{
    public interface IUsuarioService
    {
        Task<ReturnHelper<Usuario>> GetUserById(string id);
        Task<ReturnHelper<List<Usuario>>> GetUsers();

        Task<ReturnHelper<Usuario>> AddUser(UsuarioDTO usuario);
        Task<ReturnHelper<Usuario>> UpdateUser(UsuarioDTO usuario);
        Task<ReturnHelper<Usuario>> DeleteUser(string id);
        Task<ReturnHelper<Usuario>> GetUserByEmail(string correo);
        Task<ReturnHelper<Usuario>> GetUserByToken(string token);
        
    }
}
