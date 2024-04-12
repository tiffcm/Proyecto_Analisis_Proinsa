using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proinsa_Gestion_Personal.Controllers
{
    public class NominasController : Controller
    {
        // GET: Nominas
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Ingresos()
        {
            return View();
        }

        public ActionResult Deducciones()
        {
            return View();
        }
    }
}