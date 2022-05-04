using System.Collections.Generic;
using System.Threading.Tasks;
using TokaEntidad;

namespace TokaRepositorio.Interface
{
    public interface IRepositorioUsuario
    {
        Task<Usuario> GetUserById(string id);
        Task<Usuario> GetUserByEmail(string correo); 
        Task<Usuario> GetUserByToken(string token);
        Task<List<Usuario>> GetUser();

        void  AddUser(Usuario user);
        void DeleteUser(string id);
    }
}
