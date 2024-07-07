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

        public IActionResult AproCambioHorario(long ID_SOLICITUD)
        {
            long? idEmpleado = HttpContext.Session.GetInt32("ID_EMPLEADO");
            var datosDetalle = iAprobacionModel.ObtenerAprobacionPendienteDetalle(idEmpleado, ID_SOLICITUD);

            var viewModel = new AprobacionViewModel();

            if (datosDetalle != null)
            {
                viewModel.AprobacionDetalles = JsonSerializer.Deserialize<List<AprobacionDetalle>>((JsonElement)datosDetalle.CONTENIDO!);
            }

            // Si necesitas también obtener el flujo inicial al cargar esta vista:
            var datosFlujo = iAprobacionModel.ObtenerAprobacionFlujo(ID_SOLICITUD);
            if (datosFlujo != null && datosFlujo.CONTENIDO != null)
            {
                viewModel.AprobacionFlujos = JsonSerializer.Deserialize<List<AprobacionFlujo>>((JsonElement)datosFlujo.CONTENIDO!);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Aprobaciones(AprobacionViewModel viewModel)
        {
            if (viewModel.AprobacionDetalles != null && viewModel.AprobacionDetalles.Any())
            {                
                foreach (var detalle in viewModel.AprobacionDetalles)
                {
                    var entidad = new ActualizacionAprobacion
                    {
                        // Acá se recuperan los datos que vienen de la vista de aprobaciones
                        ID_EMPLEADO = detalle.ID_EMPLEADO,
                        ID_SOLICITUD = detalle.ID_SOLICITUD,
                        RESPUESTASOLICITUD = detalle.RESPUESTASOLICITUD,
                        COMENTARIO = detalle.JUSTIFICACION
                    };


                    //Ejemplo de cómo se vería el llamado desde el controlador
                    var actualizarAprobacion = iAprobacionModel.ActualizarApro(entidad);
                    if (actualizarAprobacion != null)
                    {
                        return RedirectToAction("Aprobaciones", "Aprobaciones");
                    }
                    else
                    {
                        ViewBag.Mensaje = actualizarAprobacion!.MENSAJE;
                        return View();
                    }
                }

                return RedirectToAction("Aprobaciones", "Aprobaciones"); //Esto hay que ajustarlo
            }

            return RedirectToAction("Aprobaciones", "Aprobaciones"); //Esto hay que ajustarlo
        }


        [HttpGet]
        public IActionResult ConsultarAprobDetalle(long ID_SOLICITUD)
        {
            long? idEmpleado = HttpContext.Session.GetInt32("ID_EMPLEADO");
            var datosDetalle = iAprobacionModel.ObtenerAprobacionPendienteDetalle(idEmpleado, ID_SOLICITUD);

            var viewModel = new AprobacionViewModel();

            if (datosDetalle != null)
            {
                viewModel.AprobacionDetalles = JsonSerializer.Deserialize<List<AprobacionDetalle>>((JsonElement)datosDetalle.CONTENIDO!);
            }

            // Si necesitas también obtener el flujo inicial al cargar esta vista:
            var datosFlujo = iAprobacionModel.ObtenerAprobacionFlujo(ID_SOLICITUD);
            if (datosFlujo != null && datosFlujo.CONTENIDO != null)
            {
                viewModel.AprobacionFlujos = JsonSerializer.Deserialize<List<AprobacionFlujo>>((JsonElement)datosFlujo.CONTENIDO!);
            }

            return View(viewModel);
        }



        [HttpGet]
        public IActionResult ObtenerAprobacionFlujo(long ID_SOLICITUD)
        {
            long? idEmpleado = HttpContext.Session.GetInt32("ID_EMPLEADO");
            var datosDetalle = iAprobacionModel.ObtenerAprobacionPendienteDetalle(idEmpleado, ID_SOLICITUD);
            var datosFlujo = iAprobacionModel.ObtenerAprobacionFlujo(ID_SOLICITUD);

            var viewModel = new AprobacionViewModel();

            if (datosDetalle != null)
            {
                viewModel.AprobacionDetalles = JsonSerializer.Deserialize<List<AprobacionDetalle>>((JsonElement)datosDetalle.CONTENIDO!);
            }

            if (datosFlujo != null && datosFlujo.CONTENIDO != null)
            {
                viewModel.AprobacionFlujos = JsonSerializer.Deserialize<List<AprobacionFlujo>>((JsonElement)datosFlujo.CONTENIDO!);
            }

            return View("ConsultarAprobDetalle", viewModel);
        }

    }
}
