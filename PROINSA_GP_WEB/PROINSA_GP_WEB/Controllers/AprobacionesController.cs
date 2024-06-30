using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Servicios;
using System.Data;

namespace PROINSA_GP_WEB.Controllers
{
    public class AprobacionesController (IAprobacionModel iAprobacionModel) : Controller
    {
        [HttpGet]
        public IActionResult Aprobaciones()
        {
            var idEmpleadoSession = HttpContext.Session.GetInt32("ID_EMPLEADO");
            int idEmpleado = idEmpleadoSession ?? 0;
            DataTable aprobaciones = iAprobacionModel.ObtenerSolicitudesEmpleado(idEmpleado); 
            return View(aprobaciones);
        }

        [HttpGet]
        public IActionResult AproVacaciones()
        {
            return View();
        }
    }
}
