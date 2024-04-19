using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proinsa_Gestion_Personal.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregarReporte()
        {
            return View();
        }
        public ActionResult ModificarReporte()
        {
            return View();
        }

        public ActionResult DetallarReporte()
        {
            return View();
        }

        public ActionResult EliminarReporte()
        {
            return View();
        }
    }
}