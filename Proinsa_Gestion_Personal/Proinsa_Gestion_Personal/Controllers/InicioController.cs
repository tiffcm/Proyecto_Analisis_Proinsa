﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proinsa_Gestion_Personal.Controllers
{
    public class InicioController : Controller
    {
        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            return View();
        }

    }
}