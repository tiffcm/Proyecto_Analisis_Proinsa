using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_WEB.Controllers
{
    public class SolicitudesController : Controller
    {
        [HttpGet]
        public IActionResult Vacaciones()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Constancias()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CambiosHorario()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Permisos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InformacionPersonal()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ReporteSolicitudAntiguedad()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HistorialSolicitudes()
        {
            return View();
        }
    }
}