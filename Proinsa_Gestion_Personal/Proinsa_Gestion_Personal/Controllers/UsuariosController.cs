using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proinsa_Gestion_Personal.Controllers
{
    public class UsuariosController : Controller
    {
        [HttpGet]
        public ActionResult MiCuenta()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AdministrarUsuarios() 
        {
            return View();
        }
    }
}