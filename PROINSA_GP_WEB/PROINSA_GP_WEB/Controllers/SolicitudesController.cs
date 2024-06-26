using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;

namespace PROINSA_GP_WEB.Controllers
{
    public class SolicitudesController (ISolicitudModel iSolicitudModel): Controller
    {



        [HttpGet]
        public IActionResult RegistrarSolicitud()
        {
            CargarTiposSolicitud();
            ObtenerIdEmpleado();
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


        public ActionResult CargarTiposSolicitud()
        {
            var tipoSolicitudList = iSolicitudModel.ObtenerTipoSolicitud();
            ViewBag.TipoSolicitudList = tipoSolicitudList;

            var viewModel = new Solicitud
            {
                ID = 2 
            };

            return View(viewModel);
        }


        public ActionResult ObtenerIdEmpleado()
        {
            
            var ID_EMPLEADO = HttpContext.Session.GetInt32("ID_EMPLEADO");

            
            var viewModel = new Solicitud
            {
                SOLICITANTE_ID = ID_EMPLEADO ?? 0 
            };

            return View(viewModel);
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