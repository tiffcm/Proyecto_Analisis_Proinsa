using Microsoft.AspNetCore.Mvc;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Models;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class CapacitacionesController : Controller
    {
        [Seguridad]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult AgregarCapacitacion()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult ModificarCapacitacion()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult DetallarCapacitacion()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult EliminarCapacitacion()
        {
            return View();
        }
    }
}
