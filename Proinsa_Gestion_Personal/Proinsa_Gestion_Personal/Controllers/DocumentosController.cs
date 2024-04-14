using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proinsa_Gestion_Personal.Controllers
{
    public class DocumentosController : Controller
    {
        [HttpGet]
        public ActionResult DocusPlanos()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DocusPropios()
        {
            return View();
        }
    }
}