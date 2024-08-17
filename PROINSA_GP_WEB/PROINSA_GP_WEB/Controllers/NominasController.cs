using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;

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
        public IActionResult RegistrarIngresos()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult RegistrarDeduccion()
        {
            return View();
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
