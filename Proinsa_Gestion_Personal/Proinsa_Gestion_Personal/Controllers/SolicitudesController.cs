using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proinsa_Gestion_Personal.Controllers
{
    public class SolicitudesController : Controller
    {
        [HttpGet]
        public ActionResult Vacaciones()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Constancias()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CambiosHorario()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Permisos()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InformacionPersonal()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReporteSolicitudAntiguedad()
        {
            return View();
        }

        [HttpGet]
        public ActionResult HistorialSolicitudes()
        {
            return View();
        }
    }
}