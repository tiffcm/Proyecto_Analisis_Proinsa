using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Net;

namespace PROINSA_GP_WEB.Controllers
{
    public class ReportController(IReportModel _iReportModel) : Controller
    {
        

        [Seguridad]
        [HttpGet]
        public IActionResult DownloadReport()
        {

            long? ID_EMPLEADO = HttpContext.Session.GetInt32("ID_EMPLEADO");
            if (ID_EMPLEADO == null)
            {
                return RedirectToAction("Login"); // Redirige a login si el usuario no está autenticado
            }

            /// Se deben llamar los SPs para las vistas de reportes

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DownloadReportExcel(string reportName)
        {
            var reportUrl = $"http://tc-hp-cnd2016fn/ReportServer?/GestionPersonal/{reportName}&rs:Command=Render&rs:Format=EXCELOPENXML";
            //url del servidor de reportes 

            var handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential("", "", "")

                // colocar aca nombre de usuario , contrasenna y dominio de la maquina
            };


            using (var client = new HttpClient(handler))
            {
                try
                {
                    var response = await client.GetAsync(reportUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsByteArrayAsync();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteEmpleados.xlsx");
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


        [HttpGet]
        public async Task<IActionResult> DownloadReportPdf(string reportName)
        {
            var reportUrl = $"http://tc-hp-cnd2016fn/ReportServer?/GestionPersonal/{reportName}&rs:Command=Render&rs:Format=PDF";

            var handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential("", "", "")
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

        ///// SPs para la vista
        ///





    }
}

