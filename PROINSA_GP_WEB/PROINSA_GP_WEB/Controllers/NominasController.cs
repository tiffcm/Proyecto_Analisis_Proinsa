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
                return RedirectToAction("ObtenerNominaMensualEmpleados");
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
            DateTime ultimoDiaMes = UltimoDiaMesActual();
            var recalculo = iNominaModel.CalculoNominaFinal(ultimoDiaMes);

            return RedirectToAction("ObtenerNominaMensualEmpleados");
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
            DateTime ultimoDiaMes = UltimoDiaMesActual();
            var recalculo = iNominaModel.CalculoNominaFinal(ultimoDiaMes);

            return RedirectToAction("ObtenerNominaMensualEmpleados");
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult ObtenerNominaEmpleado(int q)
        {
            // Obtener el nombre del empleado
            var respuestaNombreEmpleado = iNominaModel.ConsultarNombreEmpleado(q);
            string nombreEmpleado = string.Empty;
            DateTime fechaNomina = UltimoDiaMesActual();

            if (respuestaNombreEmpleado.CODIGO == 1)
            {
                var datosEmpleado = JsonSerializer.Deserialize<Nomina>((JsonElement)respuestaNombreEmpleado.CONTENIDO!);
                nombreEmpleado = datosEmpleado!.NOMBRE;
            }

            // Obtener el detalle de la nómina del empleado
            var respuestaDetalleNomina = iNominaModel.ObtenerNominaEmpleado(q);
            if (respuestaDetalleNomina.CODIGO == 1)
            {
                var detalleNomina = JsonSerializer.Deserialize<List<IngresosDeduccionesDetalle>>((JsonElement)respuestaDetalleNomina.CONTENIDO!);

                if (detalleNomina != null)
                {
                    // Crear un ViewModel que contenga la lista y la información adicional
                    var viewModel = new NominaViewModel
                    {
                        NOMBRE_EMPLEADO = nombreEmpleado,
                        FECHA_NOMINA = fechaNomina,
                        DetalleNomina = detalleNomina
                    };

                    return View(viewModel);
                }
            }

            return RedirectToAction("Principal", "Home");
        }


        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult ObtenerNominaMensualEmpleados()
        {
            DateTime ultimoDiaMes = UltimoDiaMesActual();
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
        [HttpPost]
        public IActionResult RevisionNomina(string observaciones)
        {
            Nomina entidad = new Nomina();
            entidad.OBSERVACIONES = observaciones;
            var respuesta = iNominaModel.RevisionNomina(entidad);
            if (respuesta!.CODIGO == 1)
            {
                return RedirectToAction("ObtenerNominaMensualEmpleados");
            }
            //Acá hay que agregar un msj con un ViewBag
            return RedirectToAction("ObtenerNominaMensualEmpleados");
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult AprobacionNomina()
        {
            Nomina entidad = new Nomina();
            entidad.ID_EMPLEADO = HttpContext.Session.GetInt32("ID_EMPLEADO")!.Value;
            var respuesta = iNominaModel.AprobacionNomina(entidad);
            if (respuesta!.CODIGO == 1)
            {
                return RedirectToAction("ObtenerNominaMensualEmpleados");
            }
            //Acá hay que agregar un msj con un ViewBag
            return RedirectToAction("ObtenerNominaMensualEmpleados");
        }

        private static DateTime UltimoDiaMesActual()
        {
            var fechaActual = DateTime.Now;
            var ultimoDiaMes = new DateTime(fechaActual.Year, fechaActual.Month, 1).AddMonths(1).AddDays(-1);
            return ultimoDiaMes;
        }
    }
}
