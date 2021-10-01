using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Control_Transporte.Models;

namespace Control_Transporte.Controllers
{
    public class PilotoController : Controller
    {
        public void LlenaTipos()
        {
            List<SelectListItem> listaTipos = new List<SelectListItem>();
            IEnumerable<TipoEmpleadoModel> tipos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/");
                var responseTask = client.GetAsync("TipoEmpleado");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<IList<TipoEmpleadoModel>>();
                    read.Wait();

                    tipos = read.Result;

                    foreach (TipoEmpleadoModel tipo in tipos)
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
                    tipos = Enumerable.Empty<TipoEmpleadoModel>();
                    ModelState.AddModelError(string.Empty, "Ha Ocurrido un Error.");
                }

            }
        }


        // GET: Piloto
        public ActionResult Index()
        {
            IEnumerable<EmpleadoModel> empleados = null;
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

                    empleados = read.Result;
                }
                else
                {
                    empleados = Enumerable.Empty<EmpleadoModel>();
                    ModelState.AddModelError(string.Empty, "Ha Ocurrido un Error.");
                }

            }
                return View(empleados);
        }

        public ActionResult Create()
        {
            LlenaTipos();
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmpleadoModel empleado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/Empleado");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EmpleadoModel>("Empleado", empleado);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Ha Ocurrido un Error.");

            return View(empleado);
        }

        public ActionResult Edit(int id)
        {
            EmpleadoModel empleado = null;
            LlenaTipos();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Empleado?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EmpleadoModel>();
                    readTask.Wait();

                    empleado = readTask.Result;
                }
            }

            return View(empleado);
        }

        [HttpPost]
        public ActionResult Edit(EmpleadoModel empleado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/Empleado");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<EmpleadoModel>("Empleado", empleado);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(empleado);
        }


        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63514/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Empleado/" + id.ToString());
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