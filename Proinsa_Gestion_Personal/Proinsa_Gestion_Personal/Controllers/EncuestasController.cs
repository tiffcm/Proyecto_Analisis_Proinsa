using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proinsa_Gestion_Personal.Controllers
{
    public class EncuestasController : Controller
    {
        [HttpGet]
        public ActionResult CrearEncuesta()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LlenarEncuesta()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InformeEncuesta()
        {
            return View();
        }
    }
}