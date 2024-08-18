using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class NominasController(INominaModel iNominaModel) : Controller
    {        
        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult RegistrarNomina()
        {
            var tiposNomina = iNominaModel.ConsultarTiposNomina();
            ViewBag.tiposNomina = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)tiposNomina!.CONTENIDO!);
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpPost]
        public IActionResult RegistrarNomina(Nomina entidad)
        {
            entidad.CreadorID = HttpContext.Session.GetInt32("ID_EMPLEADO")!.Value;
            var respuesta = iNominaModel.RegistrarNomina(entidad);
            if (respuesta!.CODIGO == 1) 
            return RedirectToAction("Principal", "Home");
            else
                return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult RegistrarIngresos(long q)
        {
            var respuesta = iNominaModel.ConsultarNombreEmpleado(q);
            if (respuesta!.CODIGO == 1)
            {
                var tiposIngresos = iNominaModel.ObtenerIngresos();
                ViewBag.tiposIngresos = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)tiposIngresos!.CONTENIDO!);
                var datos = JsonSerializer.Deserialize<IngresoNominaDetalle>((JsonElement)respuesta.CONTENIDO!);
                return View(datos);
            }
            else                            
                return RedirectToAction("Principal", "Home");
        }

        [Seguridad]
        [Administrador]
        [HttpPost]
        public IActionResult RegistrarIngresos(List<IngresoNominaDetalle> Ingresos, long empleadoId, string nombre)
        {
            if (Ingresos == null || !Ingresos.Any())
            {
                ViewBag.Mensaje = "No se recibieron ingresos para registrar.";
                return View(Ingresos);
            }

            foreach (var ingreso in Ingresos)
            {
                ingreso.EMPLEADO_ID = empleadoId;
            }
            
            var respuesta = iNominaModel.RegistrarIngresosNominaDetalle(Ingresos);

            if (respuesta!.CODIGO != 1)
            {
                ViewBag.Mensaje = "Hubo un problema al registrar los ingresos.";
                return View(Ingresos);
            }
            var fechaActual = DateTime.Now;
            var ultimoDiaMes = new DateTime(fechaActual.Year, fechaActual.Month, 1).AddMonths(1).AddDays(-1);
            var recalculo = iNominaModel.CalculoNominaFinal(ultimoDiaMes);

            return RedirectToAction("Principal", "Home");
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult RegistrarDeduccion(long q)
        {
            var respuesta = iNominaModel.ConsultarNombreEmpleado(q);
            if (respuesta!.CODIGO == 1)
            {
                var tiposDeducciones = iNominaModel.ObtenerDeducciones();
                ViewBag.tiposDeducciones = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)tiposDeducciones!.CONTENIDO!);
                var datos = JsonSerializer.Deserialize<DeduccionNominaDetalle>((JsonElement)respuesta.CONTENIDO!);
                return View(datos);
            }
            else
                return RedirectToAction("Principal", "Home");
        }

        [Seguridad]
        [Administrador]
        [HttpPost]
        public IActionResult RegistrarDeduccion(List<DeduccionNominaDetalle> Deducciones, long empleadoId, string nombre)
        {
            if (Deducciones == null || !Deducciones.Any())
            {
                ViewBag.Mensaje = "No se recibieron deducciones para registrar.";
                return View(Deducciones);
            }

            foreach (var ingreso in Deducciones)
            {
                ingreso.EMPLEADO_ID = empleadoId;
            }

            var respuesta = iNominaModel.RegistrarDeduccionNominaDetalle(Deducciones);

            if (respuesta!.CODIGO != 1)
            {
                ViewBag.Mensaje = "Hubo un problema al registrar las deducciones.";
                return View(Deducciones);
            }
            var fechaActual = DateTime.Now;
            var ultimoDiaMes = new DateTime(fechaActual.Year, fechaActual.Month, 1).AddMonths(1).AddDays(-1);
            var recalculo = iNominaModel.CalculoNominaFinal(ultimoDiaMes);

            return RedirectToAction("Principal", "Home");
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult ObtenerNominaEmpleado()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult ObtenerNominaMensualEmpleados()
        {
            var fechaActual = DateTime.Now;
            var ultimoDiaMes = new DateTime(fechaActual.Year, fechaActual.Month, 1).AddMonths(1).AddDays(-1);
            var respuesta = iNominaModel.ObtenerNominaMensualEmpleados(ultimoDiaMes);
            if (respuesta!.CODIGO == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Nomina>>((JsonElement)respuesta.CONTENIDO!);
                return View(datos);
            }
            else
            {
                return View();
            }
            
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult RevisionNomina()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult AprobacionNomina()
        {
            return View();
        }
    }
}
