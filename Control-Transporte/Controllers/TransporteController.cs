using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Control_Transporte.Models;

namespace Control_Transporte.Controllers
{
    public class TransporteController : Controller
    {

        // METODOS
        public void LlenaEmpleados()
        {
            List<SelectListItem> listaEmpleado = new List<SelectListItem>();
            IEnumerable<EmpleadoModel> pilotos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/");
                var responseTask = client.GetAsync("Empleado");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<IList<EmpleadoModel>>();
                    read.Wait();

                    pilotos = read.Result;

                    foreach (EmpleadoModel piloto in pilotos)
                    {
                        listaEmpleado.Add(new SelectListItem() { 
                            Value = piloto.iidPiloto.ToString(),
                            Text = piloto.nombre_apellido
                        });
                    }
                    ViewBag.ListaPilotos = listaEmpleado;
                }
                else
                {
                    pilotos = Enumerable.Empty<EmpleadoModel>();
                    ModelState.AddModelError(string.Empty, "Ha Ocurrido un Error.");
                }

            }
        }


        public void LlenaTipos()
        {
            List<SelectListItem> listaTipos = new List<SelectListItem>();
            IEnumerable<TipoVehiculoModel> tipos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/");
                var responseTask = client.GetAsync("TipoVehiculo");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<IList<TipoVehiculoModel>>();
                    read.Wait();

                    tipos = read.Result;

                    foreach (TipoVehiculoModel tipo in tipos)
                    {
                        listaTipos.Add(new SelectListItem()
                        {
                            Value = tipo.iidTipo.ToString(),
                            Text = tipo.nombre_tipo
                        });
                    }
                    ViewBag.ListaTipos = listaTipos;
                }
                else
                {
                    tipos = Enumerable.Empty<TipoVehiculoModel>();
                    ModelState.AddModelError(string.Empty, "Ha Ocurrido un Error.");
                }

            }
        }


        // GET: Transporte
        public ActionResult Index()
        {
            IEnumerable<VehiculoModel> vehiculos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/");
                var responseTask = client.GetAsync("Vehiculo");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<IList<VehiculoModel>>();
                    read.Wait();

                    vehiculos = read.Result;
                }
                else
                {
                    vehiculos = Enumerable.Empty<VehiculoModel>();
                    ModelState.AddModelError(string.Empty, "Ha Ocurrido un Error.");
                }

            }
            return View(vehiculos);
        }

        public ActionResult Create()
        {
            LlenaEmpleados();
            LlenaTipos();
            return View();
        }

        [HttpPost]
        public ActionResult Create(VehiculoModel vehiculo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/Vehiculo");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<VehiculoModel>("Vehiculo", vehiculo);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Ha Ocurrido un Error.");

            return View(vehiculo);
        }


        public ActionResult Edit(int id)
        {
            VehiculoModel vehiculo = null;
            LlenaEmpleados();
            LlenaTipos();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Vehiculo?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<VehiculoModel>();
                    readTask.Wait();

                    vehiculo = readTask.Result;
                }
            }

            return View(vehiculo);
        }


        [HttpPost]
        public ActionResult Edit(VehiculoModel vehiculo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/Vehiculo");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<VehiculoModel>("Vehiculo", vehiculo);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(vehiculo);
        }


        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Vehiculo/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

    }
}