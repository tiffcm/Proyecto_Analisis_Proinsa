using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proinsa_Gestion_Personal.Controllers
{
    public class ActividadesController : Controller
    {
        [HttpGet]
        public ActionResult Horarios()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistroActividades()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GestionDocumentos()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DocumentoUsuario()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Documentoplano()
        {
            return View();
        }

        public ActionResult Aprobaciones()
        {
            return View();
        }

        public ActionResult AproVacaciones()
        {
            return View();
        }
    }
}