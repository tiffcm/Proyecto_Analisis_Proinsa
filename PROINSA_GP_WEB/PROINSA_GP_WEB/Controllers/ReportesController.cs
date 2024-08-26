using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Net;

namespace PROINSA_GP_WEB.Controllers
{
    public class ReportesController(IReporteModel _iReporteModel) : Controller
    {
        // Vista principal para los reportes
        [Seguridad]
        [HttpGet]
        public IActionResult DownloadReport()
        {
            long? ID_EMPLEADO = HttpContext.Session.GetInt32("ID_EMPLEADO");
            if (ID_EMPLEADO == null)
            {
                return RedirectToAction("Login", "Usuarios"); // Redirige a login si el usuario no está autenticado
            }

            // Llamar a los procedimientos almacenados para obtener datos necesarios para la vista, si aplica

            return View();
        }

        // Método para descargar el reporte en formato Excel
        [HttpGet]
        public async Task<IActionResult> DownloadReportExcel(string reportName)
        {
            var reportUrl = $"http://tc-hp-cnd2016fn/ReportServer?/GestionPersonal/{reportName}&rs:Command=Render&rs:Format=EXCELOPENXML";

            var handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential("username", "password", "domain") // Reemplaza con tus credenciales
            };

            using (var client = new HttpClient(handler))
            {
                try
                {
                    var response = await client.GetAsync(reportUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsByteArrayAsync();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{reportName}.xlsx");
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        return Content($"Error al obtener el reporte: {response.StatusCode} - {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    return Content($"Error al intentar obtener el reporte: {ex.Message}");
                }
            }
        }

        // Método para descargar el reporte en formato PDF
        [HttpGet]
        public async Task<IActionResult> DownloadReportPdf(string reportName)
        {
            var reportUrl = $"http://tc-hp-cnd2016fn/ReportServer?/GestionPersonal/{reportName}&rs:Command=Render&rs:Format=PDF";

            var handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential("username", "password", "domain") // Reemplaza con tus credenciales
            };

            using (var client = new HttpClient(handler))
            {
                try
                {
                    var response = await client.GetAsync(reportUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsByteArrayAsync();
                        return File(content, "application/pdf", $"{reportName}.pdf");
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        return Content($"Error al obtener el reporte: {response.StatusCode} - {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    return Content($"Error al intentar obtener el reporte: {ex.Message}");
                }
            }
        }

        // Método para previsualizar los datos del reporte en un modal
        [HttpGet]
        public IActionResult PreviewReport(string reportName, long? empleadoId)
        {
            // Aquí se llamaría al IReporteModel para obtener los datos necesarios para la previsualización.
            // Dependiendo del reporte, puedes realizar diferentes llamadas.

            Respuesta respuesta;

            switch (reportName)
            {
                case "ReporteEmpleados":
                    respuesta = _iReporteModel.DatosEmpleadoNominaReporte(empleadoId ?? 0); // Aquí se utilizaría un método adecuado
                    break;
                case "ReporteProyectos":
                    // Implementar la lógica de obtención de datos para el reporte de proyectos
                    respuesta = new Respuesta(); // Reemplaza con la llamada correcta
                    break;
                default:
                    respuesta = new Respuesta { CODIGO = 0, MENSAJE = "Reporte no encontrado" };
                    break;
            }

            if (respuesta.CODIGO == 1)
            {
                return PartialView("_PreviewReport", respuesta.CONTENIDO); // Renderizar una vista parcial con los datos
            }
            else
            {
                return Content($"Error al obtener los datos para la previsualización: {respuesta.MENSAJE}");
            }
        }
    }
}
