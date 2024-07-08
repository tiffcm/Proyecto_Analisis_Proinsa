using Microsoft.AspNetCore.Mvc;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Models;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ReportesController : Controller
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
        public IActionResult AgregarReporte()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult ModificarReporte()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult DetallarReporte()
        {
            return View();
        }

        [Seguridad]
        // [Administrador]
        [HttpGet]
        public IActionResult EliminarReporte()
        {
            return View();
        }
    }
}
