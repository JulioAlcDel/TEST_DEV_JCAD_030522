using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using RfcFacil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TokaDTOS;
using TokaInfrestructura.Halper;
using TokaInfrestructura.Comunes;
using TokaPrueba.Helper;
using TokaPrueba.Models;
using ClosedXML.Excel;
using System.IO;

namespace TokaPrueba.Controllers
{
    public class PersonaFisicaController : Controller
    {
        ApiHelper _api = new ApiHelper();
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
     
        public JsonResult CalculoRFC(RFCViewModel modelo)
        {

            var rfc = RfcBuilder.ForNaturalPerson()
                     .WithName(modelo.Nombre)
                     .WithFirstLastName(modelo.ApellidoPaterno)
                     .WithSecondLastName(modelo.ApellidoMaterno)
                     .WithDate(modelo.Fecha.Year, modelo.Fecha.Month, modelo.Fecha.Day)
                     .Build();
            return Json(new { value = rfc.ToString() });
        }
        [HttpPost]
        public async Task<IActionResult> Create(PersonaFisicaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var accessToken = HttpContext.Session.GetString("Token");
            HttpClient _client = _api.Initial();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var myContent = JsonConvert.SerializeObject(new PersonaFisicaDTO()
            {
                IdPersonaFisica = model.IdPersonaFisica.ToString(),
                Nombre = model.Nombre,
                FechaNacimiento = model.FechaNacimiento,
                RFC = model.RFC,
                ApellidoMaterno = model.ApellidoMaterno,
                ApellidoPaterno = model.ApellidoPaterno,
                Activo = model.Activo
            });
            var data = new StringContent(myContent, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await _client.PostAsync("api/personaFisica", data);
            var returs = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ReturnHelper<PersonaFisicaViewModel>>(returs);

            if (result.IsSuccessStatusCode)
            {

                TempData["UserMessage"] = response.MenssageSucess;
                TempData["Tipo"] = "Existo";

                return View(new PersonaFisicaViewModel());
            }
            TempData["UserMessage"] = response.Error.FirstOrDefault();
            TempData["Tipo"] = "Error";
            return View();
        }

        public IActionResult Edit(int id)
        {
            var model = GetPersonaFisica(id.ToString());
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PersonaFisicaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var accessToken = HttpContext.Session.GetString("Token");
            HttpClient _client = _api.Initial();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var myContent = JsonConvert.SerializeObject(new PersonaFisicaDTO()
            {
                IdPersonaFisica = model.IdPersonaFisica.ToString(),
                Nombre = model.Nombre,
                FechaNacimiento = model.FechaNacimiento,
                RFC = model.RFC,
                ApellidoMaterno = model.ApellidoMaterno,
                ApellidoPaterno = model.ApellidoPaterno,
                Activo = model.Activo
            }); ;
            var data = new StringContent(myContent, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await _client.PutAsync("api/personaFisica/" + model.IdPersonaFisica, data);
            var returs = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ReturnHelper<PersonaFisicaViewModel>>(returs);
            if (result.IsSuccessStatusCode)
            {
                TempData["UserMessage"] = response.MenssageSucess;
                TempData["Tipo"] = "Existo";
                return View(response.Value);
            }
            TempData["UserMessage"] = response.Error.FirstOrDefault();
            TempData["Tipo"] = "Error";
            return View();
        }
        public async Task<JsonResult> GetPersonasFisica()
        {
            var accessToken = HttpContext.Session.GetString("Token");
            HttpClient _client = _api.Initial();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage result = await _client.GetAsync("api/personaFisica");
            if (result.IsSuccessStatusCode)
            {
                var returs = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<ReturnHelper<List<PersonaFisicaViewModel>>>(returs);
                return Json(new { data = response.Value });
            }

            return Json(new { data = new ReturnHelper<List<PersonaFisicaViewModel>>().Value });
        }
        public async Task<List<PersonaFisicaViewModel>> ListPersonasFisica()
        {
            var accessToken = HttpContext.Session.GetString("Token");
            HttpClient _client = _api.Initial();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage result = await _client.GetAsync("api/personaFisica");
            var returs = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ReturnHelper<List<PersonaFisicaViewModel>>>(returs);
            if (result.IsSuccessStatusCode)
            {

                return response.Value;
            }

            return response.Value;
        }

        [HttpPost]
        public async Task<JsonResult> DeletePersonaFisica(int id)
        {
            var accessToken = HttpContext.Session.GetString("Token");
            HttpClient _client = _api.Initial();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage result = await _client.DeleteAsync($"api/personaFisica/{id}");
            var returs = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ReturnHelper<PersonaFisicaViewModel>>(returs);
            if (result.IsSuccessStatusCode)
            {
                return Json(new { data = response.Value, Success = response.MenssageSucess, Status = true, type = 2 });
            }

            return Json(new { data = response.Value, Success = response.Error.FirstOrDefault(), Status = true, type = 2 });
        }
        public PersonaFisicaViewModel GetPersonaFisica(string id)
        {
            var accessToken = HttpContext.Session.GetString("Token");
            HttpClient _client = _api.Initial();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage result = _client.GetAsync("api/personaFisica/" + id).Result;
            var returs = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ReturnHelper<PersonaFisicaViewModel>>(returs);
            if (result.IsSuccessStatusCode)
            {
                return response.Value;
            }

            return response.Value;
        }

        public async Task<FileResult> ExportExcel()
        {
            var lista = ListPersonasFisica();
            var dataTable = new DatatableConvert().ToDataTable(lista.Result);
            using (XLWorkbook libro = new XLWorkbook())
            {
                var hoja = libro.Worksheets.Add(dataTable);

                hoja.ColumnsUsed().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    libro.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte " + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }

    }
}
