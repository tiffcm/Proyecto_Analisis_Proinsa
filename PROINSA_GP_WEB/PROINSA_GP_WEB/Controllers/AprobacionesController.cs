using Azure;
using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Data;
using System.Text.Json;

namespace PROINSA_GP_WEB.Controllers
{
    public class AprobacionesController (IAprobacionModel iAprobacionModel) : Controller
    {
        [HttpGet]
        public IActionResult Aprobaciones()
        {
            ObtenerAprobacionFlujo(3);
            long? idEmpleado = HttpContext.Session.GetInt32("ID_EMPLEADO");
            var datos = iAprobacionModel.ObtenerSolicitudesEmpleado(idEmpleado);
            if (datos != null)
            {
                var aprobaciones = JsonSerializer.Deserialize<List<Aprobacion>>((JsonElement)datos.CONTENIDO!);
                return View(aprobaciones);
            }
            else
            return View();
        }


        [HttpGet]
        public IActionResult AproCambioHorario(long ID_SOLICITUD)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConsultarAprobDetalle(long ID_SOLICITUD)
        {
            long? idEmpleado = HttpContext.Session.GetInt32("ID_EMPLEADO");
            var datos = iAprobacionModel.ObtenerAprobacionPendienteDetalle(idEmpleado, ID_SOLICITUD);

            if (datos != null && datos.CONTENIDO != null)
            {
                var detalle = JsonSerializer.Deserialize<List<AprobacionDetalle>>((JsonElement)datos.CONTENIDO!);
                // se debe llamar el flujo 
                return Json(detalle);
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult ObtenerAprobacionFlujo(long ID_SOLICITUD)
        {            
            var datos = iAprobacionModel.ObtenerAprobacionFlujo(ID_SOLICITUD);

            if (datos != null && datos.CONTENIDO != null)
            {
                var detalle = JsonSerializer.Deserialize<List<AprobacionDetalle>>((JsonElement)datos.CONTENIDO!);
                return Json(detalle);
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}
