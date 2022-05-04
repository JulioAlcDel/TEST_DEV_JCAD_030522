using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokaDTOS
{
    public class ErrorDTO
    {
        public string MensajeError { get; set; }
        public int Error {get; set;}

        public bool Success
        {
            get
            {
                if (Error < 0)
                {
                    return false;

                }
                else
                {
                    return true;
                }


            }
        }
    }
}
