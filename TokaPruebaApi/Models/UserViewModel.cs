using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaPruebaApi.Models
{
    public class UserViewModel
    {
        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Token { get; set; }
    }
}
