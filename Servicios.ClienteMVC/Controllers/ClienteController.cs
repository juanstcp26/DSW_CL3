using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using Servicios.Dominio;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Servicios.ClienteMVC.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:44396/");

            var request = cliente.GetAsync("api/Cliente").Result;

            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                var listado = JsonConvert.DeserializeObject<List<clientes>>(resultado);

                return View(listado.ToList());
            }
            return View();
        }

        public ActionResult Nuevo()
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:44396/");

            var request = cliente.GetAsync("api/Pais").Result;

            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                var listado = JsonConvert.DeserializeObject<List<paises>>(resultado);
                ViewBag.paises = new SelectList(listado.ToList(), "Idpais", "NombrePais");
                return View();
            }
            
            return View(new clientes());
        }

        [HttpPost]
        public ActionResult Nuevo(clientes cli)
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:44396/");

            var request = cliente.PostAsync("api/Cliente", cli, new JsonMediaTypeFormatter()).Result;

            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                var estado = JsonConvert.DeserializeObject<bool>(resultado);

                if (estado == true)
                {
                    return RedirectToAction("Index");
                }
                return View(cli);
            }
            return View(cli);
        }

        [HttpGet]
        public ActionResult Actualizar(string cod)
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:44396/");

            var request2 = cliente.GetAsync("api/Pais").Result;

            if (request2.IsSuccessStatusCode)
            {
                var resultado2 = request2.Content.ReadAsStringAsync().Result;
                var listado = JsonConvert.DeserializeObject<List<paises>>(resultado2);
                ViewBag.paises = new SelectList(listado.ToList(), "Idpais", "NombrePais");

                var request = cliente.GetAsync("api/Cliente?cod=" + cod).Result;
                if (request.IsSuccessStatusCode)
                {
                    var resultado = request.Content.ReadAsStringAsync().Result;
                    var registro = JsonConvert.DeserializeObject<clientes>(resultado);

                    return View(registro);
                }
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Actualizar(clientes cli)
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:44396/");

            var request = cliente.PutAsync("api/Cliente", cli, new JsonMediaTypeFormatter()).Result;

            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                var estado = JsonConvert.DeserializeObject<bool>(resultado);

                if (estado == true)
                {
                    return RedirectToAction("Index");
                }
                return View(cli);
            }
            return View(cli);
        }

        [HttpGet]
        public ActionResult Eliminar(string cod)
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:44396/");

            var request = cliente.DeleteAsync("api/Cliente?cod=" + cod).Result;

            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                var estado = JsonConvert.DeserializeObject<bool>(resultado);

                if (estado == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}