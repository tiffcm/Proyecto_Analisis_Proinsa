using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;

namespace PROINSA_GP_WEB.Controllers
{
    public class SolicitudesController (ISolicitudModel iSolicitudModel): Controller
    {
        [HttpGet]
        public IActionResult RegistrarSolicitud()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarSolicitud(Solicitud ent)
        {
            var resp = iSolicitudModel.RegistrarSolicitud(ent);

            if (resp.CODIGO == 1)
                return RedirectToAction("Index", "Home");

            ViewBag.msj = resp.MENSAJE;
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