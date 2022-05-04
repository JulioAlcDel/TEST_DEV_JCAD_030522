using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokaEntidad
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Correo { get; set; }

        public string Clave { get; set; }

        public string Sal { get; set; }

        public string Token { get; set; }
    }
}
