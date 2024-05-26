using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_WEB.Controllers
{
    public class CapacitacionesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AgregarCapacitacion()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ModificarCapacitacion()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DetallarCapacitacion()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EliminarCapacitacion()
        {
            return View();
        }
    }
}
