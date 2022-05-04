using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaPruebaApi.Models
{
    public class LoginViewModel
    {
        public string Correo { get; set; }

        public string Clave { get; set; }
        public string Sal { get; set; }

        public string Token { get; set; }

    }
}
