using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_WEB.Controllers
{
    public class NominasController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Ingresos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GestionIngresos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Deducciones()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GestionDeducciones()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Configuracion()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Calculo()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NominaMensual()
        {
            return View();
        }
    }
}
