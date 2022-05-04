using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TokaDTOS;
using TokaInfrestructura.Halper;
using TokaPrueba.Helper;
using TokaPrueba.Models;

namespace TokaPrueba.Controllers
{
    public class UsuarioController : Controller
    {
        ApiHelper _api = new ApiHelper();
        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            HttpClient _client = _api.Initial();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var myContent = JsonConvert.SerializeObject(new UsuarioDTO() { Clave = modelo.Password, Correo = modelo.Email });
            var data = new StringContent(myContent, Encoding.UTF8, "application/json");          
            HttpResponseMessage result = await _client.PostAsync("api/usuario", data);
            if (result.IsSuccessStatusCode) {      
                
                return RedirectToAction("Login", "Usuario");
            }
            return View(modelo);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            HttpClient _client = _api.Initial();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var myContent = JsonConvert.SerializeObject(new UsuarioDTO() { Clave = modelo.Password, Correo = modelo.Email });
            var data = new StringContent(myContent, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await _client.PostAsync("api/login", data);
            if (result.IsSuccessStatusCode)
            {
                var returs = result.Content.ReadAsStringAsync().Result;
                HttpContext.Session.SetString("Token", JsonConvert.DeserializeObject<string>(returs));
                return RedirectToAction("Index", "PersonaFisica");
            }

            return View(modelo); ;
        }
    }
}
