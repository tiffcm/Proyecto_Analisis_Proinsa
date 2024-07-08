using Microsoft.AspNetCore.Mvc;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Models;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class NominasController : Controller
    {
        // VALIDACION DE VISTAS Y FUNCIONES POR TRABAJAR
        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult Ingresos()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult GestionIngresos()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult Deducciones()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult GestionDeducciones()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult Configuracion()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult Calculo()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult NominaMensual()
        {
            return View();
        }
    }
}
