using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TokaPrueba.Helper
{
    public class ApiHelper
    {
        public HttpClient Initial() {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44317");
               
            return client;
        
        }
    }
}
